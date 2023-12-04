using AutoMapper;
using Infrastructure.ApiResponses;
using Infrastructure.Mail.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SASSTS.Business.Controls.Dto;
using SASSTS.Business.CustomExceptions;
using SASSTS.Business.Interfaces;
using SASSTS.Business.MessageServices.Interfaces;
using SASSTS.Business.Security.JWT;
using SASSTS.Business.Security.Password;
using SASSTS.DataAccess.EntityFramework.Context;
using SASSTS.DataAccess.UnitOfWork;
using SASSTS.Model.Dtos.Accounting;
using SASSTS.Model.Entities;
using SASSTS.Model.RequestModels.AccountingVM;
using SASSTS.Model.RequestModels.EmployeeVM;

namespace SASSTS.Business.Implementations
{
    public class AccountingBs : IAccountingBs
    {
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountingBs> _logger;
        private readonly IConfiguration _configuration;
        private readonly SASSTSDataContext _context;
        private readonly IMailService _mailService;
        private readonly IMessageService _messageService;
        private readonly LanguageService _languageService;
        public AccountingBs(IUnitWork unitWork, IMapper mapper, ILogger<AccountingBs> logger, IConfiguration configuration, SASSTSDataContext context, IMailService mailService, IMessageService messageService, LanguageService languageService)
        {
            _configuration = configuration;
            _unitWork = unitWork;
            _mapper = mapper;
            _logger = logger;
            _context = context;
            _mailService = mailService;
            _messageService = messageService;
            _languageService = languageService;
        }



        //Kişisel hesap için servis buradan gidiyor elleme.
        public async Task<ApiResponse<EmployeeandAccountModel>> GetAccountbyAccountingId(int accountingId, string include)
        {
            var model = await _unitWork.GetRepository<Accounting>().GetAsync(x => x.Id == accountingId, includeList: include);
            var k = CipherUtil.DecryptString(_configuration["AppSettings:SecretKey"], model.UserPassword);
            var dto = _mapper.Map<EmployeeandAccountModel>(model);
            dto.UserPassword = k;
            return ApiResponse<EmployeeandAccountModel>.Success(StatusCodes.Status200OK, dto);
        }




        public async Task<ApiResponse<List<AccountingGetDto>>> GetAccounting(string include) =>
            await ExceptionValidDto<Accounting, AccountingGetDto, AccountingBs>.GetAllDtosController(_unitWork, "Accounting", logger: _logger, mapper: _mapper,
                include: include);

        public async Task<ApiResponse<List<AccountingGetDto>>> GetAllbyCompanyId(int id, string include)
        {
            var employees = await _unitWork.GetRepository<Employee>().GetAllAsync(x => x.CompanyId == id);
            List<Accounting> accountings = new List<Accounting>();
            foreach (var employee in employees)
            {
                accountings.Add(await _unitWork.GetRepository<Accounting>().GetAsync(x => x.EmployeeId == employee.Id, include));
            }
            var returnList = _mapper.Map<List<AccountingGetDto>>(accountings);
            return ApiResponse<List<AccountingGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }


        public async Task<ApiResponse<RandomNumber>> LoginTwoFactor(LoginVM loginVM)
        {
            var result = new ApiResponse<RandomNumber>();

            var hashedPassword = CipherUtil.EncryptString(_configuration["AppSettings:SecretKey"], loginVM.UserPassword);

            var existsAccounting = await _unitWork.GetRepository<Accounting>().GetSingleByFilterAsync(x => x.UserPassword == hashedPassword && x.Email == loginVM.UserData || x.UserPassword == hashedPassword && x.UserName == loginVM.UserData, "Employee");

            if (existsAccounting is null)
            {
                return ApiResponse<RandomNumber>.Fail(StatusCodes.Status401Unauthorized, "Personel bilgileri bulunamadı ya da parola hatalıdır.");
            }
            var employeeExists = await _unitWork.GetRepository<Employee>().GetById(existsAccounting.EmployeeId);
            var number = RandomNumberMetot();
            var randomNumberVM = new RandomNumberVM
            {
                Number = number,
                Email = existsAccounting.Email,
                UserName = existsAccounting.UserName,
            };


            var randomNumberExists = _mapper.Map<RandomNumberVM, RandomNumber>(randomNumberVM);

            _unitWork.GetRepository<RandomNumber>().Add(randomNumberExists);

            await _unitWork.CommitAsync();


            result.Data = randomNumberExists;


            _unitWork.Dispose();

            if (result.Data != null)
            {
                var bodyMessage = loginVM.Hitap + " " + employeeExists.EmployeeName + " " + employeeExists.EmployeeSurname + ". " + loginVM.LoginCodeMessage + " " + randomNumberExists.Number;
                await _mailService.SendMessageAsync($"{randomNumberExists.Email}", loginVM.SubjectMessageLogin, bodyMessage);

                return ApiResponse<RandomNumber>.Success(StatusCodes.Status200OK, result.Data);
            }


            return ApiResponse<RandomNumber>.Fail(StatusCodes.Status200OK, "hatalı kod");
        }

        public async Task<ApiResponse<AccessToken>> RandomNumberLogin(RandomNumberNoVM randomNumberNoVM)
        {
            var result = new ApiResponse<AccessToken>();
            var accountRandomNumber = await _unitWork.GetRepository<RandomNumber>().GetSingleByFilterAsync(x => x.Number == randomNumberNoVM.RandomNumberNo && x.Email == randomNumberNoVM.UserData || x.Number == randomNumberNoVM.RandomNumberNo && x.UserName == randomNumberNoVM.UserData);
            if (accountRandomNumber is null)
                return ApiResponse<AccessToken>.Fail(StatusCodes.Status401Unauthorized, "Hatalı kod.");

            var lastNumber = await _context.RandomNumbers.OrderBy(x => x.Id).LastOrDefaultAsync(x => x.Email == accountRandomNumber.Email);

            if (lastNumber.Number != randomNumberNoVM.RandomNumberNo)
            {
                return ApiResponse<AccessToken>.Fail(StatusCodes.Status401Unauthorized, "Hatalı kod.");
            }
            else
            {
                var existsAccounting = await _unitWork.GetRepository<Accounting>().GetSingleByFilterAsync(x => x.Email == lastNumber.Email || x.UserName == lastNumber.UserName);

                var existsEmployee = await _unitWork.GetRepository<Employee>().GetById(existsAccounting.EmployeeId);

                var accessToken = await new JwtGenerator(_configuration, _unitWork).CreateAccessToken(existsEmployee.Id);
                if (accessToken is not null)
                {
                    await _unitWork.GetRepository<Model.Entities.ProcessHistory>().InsertAsync(new Model.Entities.ProcessHistory
                    {
                        EmployeeId = existsEmployee.Id,
                        ProcessType = "Oturum Açma",
                        ProcessDetail = "Oturum açma işlemi başarılı bir şekilde gerçekleştirildi",
                        ProcessTime = DateTime.Now
                    });
                    return ApiResponse<AccessToken>.Success(StatusCodes.Status200OK, accessToken);
                }
                return ApiResponse<AccessToken>.Fail(StatusCodes.Status401Unauthorized, "token üretilemedi");
            }
        }

        public async Task<ApiResponse<int>> ForgottenPassword(EmployeeMailVM employeeMailVM)
        {
            var result = new ApiResponse<int>();

            var employeeMailExists = await _unitWork.GetRepository<Accounting>().GetSingleByFilterAsync(x => x.Email.Trim() == employeeMailVM.UserData.Trim());
            if (employeeMailExists is null)
            {
                throw new AlreadyExistsException($"{employeeMailVM.UserData} eposta adresi bulunamadı. Lütfen eposta adresinizi kontrol ederek tekrar deneyiniz.");
            }

            var employeeExists = await _unitWork.GetRepository<Employee>().GetById(employeeMailExists.EmployeeId);
            var number = RandomNumberMetot();
            var randomNumberVM = new RandomNumberVM
            {
                Number = number,
                Email = employeeMailVM.UserData,
                UserName = employeeMailExists.UserName,

            };


            var randomNumberExists = _mapper.Map<RandomNumberVM, RandomNumber>(randomNumberVM);
            _unitWork.GetRepository<RandomNumber>().Add(randomNumberExists);
            await _unitWork.CommitAsync();

            result.Data = randomNumberExists.Number;
            _unitWork.Dispose();


            var bodyMessage = employeeMailVM.Hitap + " " + employeeExists.EmployeeName + " " + employeeExists.EmployeeSurname + ". " + employeeMailVM.LoginCodeMessage + " " + randomNumberExists.Number;
            await _mailService.SendMessageAsync($"{randomNumberExists.Email}", employeeMailVM.SubjectMessagePassword, bodyMessage);

            return ApiResponse<int>.Success(StatusCodes.Status200OK, result.Data);

        }


        public async Task<ApiResponse<long>> RandomNumberConfirm(RandomNumberNoVM randomNumberNoVM)
        {
            var result = new ApiResponse<long>();
            var accountRandomNumber = await _unitWork.GetRepository<RandomNumber>().GetSingleByFilterAsync(x => x.Number == randomNumberNoVM.RandomNumberNo && x.Email == randomNumberNoVM.UserData || x.Number == randomNumberNoVM.RandomNumberNo && x.UserName == randomNumberNoVM.UserData);
            if (accountRandomNumber is null)
            {
                return ApiResponse<long>.Fail(StatusCodes.Status401Unauthorized, "Hatalı kod.");
            }


            var lastNumber = await _context.RandomNumbers.OrderBy(x => x.Id).LastOrDefaultAsync(x => x.Email == accountRandomNumber.Email);
            if (lastNumber.Number != randomNumberNoVM.RandomNumberNo)
            {
                return ApiResponse<long>.Fail(StatusCodes.Status401Unauthorized, "Hatalı kod.");
            }
            else
            {
                var person = await _unitWork.GetRepository<Accounting>().GetSingleByFilterAsync(x => x.Email == accountRandomNumber.Email);
                result.Data = person.Id;
                return ApiResponse<long>.Success(StatusCodes.Status200OK, result.Data);
            }
        }

        public async Task<ApiResponse<bool>> UpdateUser(UpdateUserVM updateUserVM)
        {
            var result = new ApiResponse<bool>();
            var accountingExists = await _unitWork.GetRepository<Accounting>().GetById(updateUserVM.Id);
            if (accountingExists is null)
            {
                return ApiResponse<bool>.Fail(StatusCodes.Status401Unauthorized, "Hesap bulunamadı.");
            }

            var employeeExists = await _unitWork.GetRepository<Employee>().GetById(accountingExists.EmployeeId);
            if (employeeExists is null)
            {
                return ApiResponse<bool>.Fail(StatusCodes.Status401Unauthorized, "Personel kaydı bulunamadı.");
            }

            var newPassword = updateUserVM.UserPassword;
            newPassword = CipherUtil.EncryptString(_configuration["AppSettings:SecretKey"], newPassword);

            var accountingPasswordExists = await _unitWork.GetRepository<Accounting>().AnyAsync(x => x.UserPassword == newPassword);
            if (accountingPasswordExists)
            {
                return ApiResponse<bool>.Fail(StatusCodes.Status401Unauthorized, "Eski parola ve yeni parola bilgisi aynı olamaz.");
            }

            if (employeeExists.Email != updateUserVM.Email.ToUpper())
            {
                var emailExists = await _unitWork.GetRepository<Employee>().AnyAsync(x => x.Email == updateUserVM.Email);
                if (emailExists)
                {
                    return ApiResponse<bool>.Fail(StatusCodes.Status401Unauthorized, "Girilen e-posta adresi kayıtlı.Lütfen kontrol ederek tekrar giriniz.");
                }
            }

            if (employeeExists.Phone != updateUserVM.Phone)
            {
                var phoneExists = await _unitWork.GetRepository<Employee>().AnyAsync(x => x.Phone == updateUserVM.Phone);
                if (phoneExists)
                {
                    return ApiResponse<bool>.Fail(StatusCodes.Status401Unauthorized, "Girilen telefon numarası kayıtlı.Lütfen kontrol ederek tekrar giriniz.");
                }
            }
            var updateEmployee = new UpdateUserVM()
            {
                Id = employeeExists.Id,
                Email = updateUserVM.Email,
                Phone = updateUserVM.Phone
            };
            var accountingUpdate = _mapper.Map(updateUserVM, accountingExists);
            var employeeUpdate = _mapper.Map(updateEmployee, employeeExists);

            accountingUpdate.UserPassword = CipherUtil
                .EncryptString(_configuration["AppSettings:SecretKey"], accountingUpdate.UserPassword);

            _unitWork.GetRepository<Accounting>().Update(accountingUpdate);
            _unitWork.GetRepository<Employee>().Update(employeeUpdate);
            await _unitWork.CommitAsync();

            _unitWork.Dispose();

            await _mailService.SendMessageAsync($"{accountingExists.Email}", _messageService.UpdateUser(), _messageService.UpdateUserMessage(employeeExists));
            return ApiResponse<bool>.Success(StatusCodes.Status200OK, result.Data);
        }


        public async Task<ApiResponse<bool>> UpdateEmployee(UpdateEmployeeVM updateEmployeeVM)
        {
            var result = new ApiResponse<bool>();

            var employeeExists = await _unitWork.GetRepository<Employee>().GetById(updateEmployeeVM.Id);
            if (employeeExists is null)
            {
                return ApiResponse<bool>.Fail(StatusCodes.Status401Unauthorized, "Personel kaydı bulunamadı.");
            }

            var employeeUpdate = _mapper.Map(updateEmployeeVM, employeeExists);

            _unitWork.GetRepository<Employee>().Update(employeeUpdate);
            await _unitWork.CommitAsync();

            _unitWork.Dispose();
            //await _mailService.SendMessageAsync($"{employeeExists.Email}", _messageService.UpdateUser(), $"Sayın {employeeExists.EmployeeName}+' '+{employeeExists.EmployeeSurname} kullanıcı bilgileriniz güncellenmiştir.");
            return ApiResponse<bool>.Success(StatusCodes.Status200OK, result.Data);
        }


        public async Task<ApiResponse<bool>> NewPassword(NewPasswordVM newPasswordVM)
        {
            var result = new ApiResponse<bool>();

            var randomNumberExists = await _unitWork.GetRepository<RandomNumber>().GetAsync(x => x.Number == newPasswordVM.RandomNumberNo);
            if (randomNumberExists is null)
            {
                return ApiResponse<bool>.Fail(StatusCodes.Status401Unauthorized, "Kod hatalı.");
            }
            var lastNumber = await _context.RandomNumbers.OrderBy(x => x.Id).LastOrDefaultAsync(x => x.Email == randomNumberExists.Email);

            if (lastNumber.Number != newPasswordVM.RandomNumberNo)
            {
                return ApiResponse<bool>.Fail(StatusCodes.Status401Unauthorized, "Hatalı kod.");
            }
            else
            {
                var accountingExists = await _unitWork.GetRepository<Accounting>().GetSingleByFilterAsync((x => x.Email == randomNumberExists.Email || x.UserName == randomNumberExists.UserName));
                if (accountingExists is null)
                {
                    return ApiResponse<bool>.Fail(StatusCodes.Status401Unauthorized, "Personel kaydı bulunamadı.");
                }
                var employeeExists = await _unitWork.GetRepository<Employee>().GetById(accountingExists.EmployeeId);
                var newPassword = newPasswordVM.UserPassword;
                newPassword = CipherUtil.EncryptString(_configuration["AppSettings:SecretKey"], newPassword);

                var employeePasswordExists = await _unitWork.GetRepository<Accounting>().AnyAsync(x => x.UserPassword == newPassword);
                if (employeePasswordExists)
                {
                    return ApiResponse<bool>.Fail(StatusCodes.Status401Unauthorized, "Eski parola ve yeni parola bilgisi aynı olamaz.");
                }
                var employeeUpdate = _mapper.Map(newPasswordVM, accountingExists);

                employeeUpdate.UserPassword = CipherUtil
                    .EncryptString(_configuration["AppSettings:SecretKey"], employeeUpdate.UserPassword);

                _unitWork.GetRepository<Accounting>().Update(employeeUpdate);
                await _unitWork.CommitAsync();

                _unitWork.Dispose();

                var bodyMessage = newPasswordVM.Hitap + " " + employeeExists.EmployeeName + " " + employeeExists.EmployeeSurname + ". " + newPasswordVM.NewPasswordMessage;
                await _mailService.SendMessageAsync($"{randomNumberExists.Email}", newPasswordVM.SubjectMessagePassword, bodyMessage);

                //await _mailService.SendMessageAsync($"{employeeExists.Email}", _messageService.SubjectMessagePassword(), _messageService.NewPasswordMessage(employeeExists));


                return ApiResponse<bool>.Success(StatusCodes.Status200OK, result.Data);
            }
        }

        public async Task<ApiResponse<bool>> DeleteRandomNumber(NewPasswordVM newPasswordVM)
        {
            var result = new ApiResponse<bool>();

            var accountingExists = await _unitWork.GetRepository<Accounting>().GetSingleByFilterAsync(x => x.Id == newPasswordVM.Id);

            var randomNumberMail = await _unitWork.GetRepository<RandomNumber>().GetSingleByFilterAsync(x => x.Email == accountingExists.Email && x.Number == newPasswordVM.RandomNumberNo);
            var lastNumber = await _context.RandomNumbers.OrderBy(x => x.Id).LastOrDefaultAsync(x => x.Email == randomNumberMail.Email);

            if (lastNumber.Number != newPasswordVM.RandomNumberNo)
                return ApiResponse<bool>.Fail(StatusCodes.Status401Unauthorized, "Hatalı kod.");
            else
            {
                _unitWork.GetRepository<RandomNumber>().DeleteAllItems(x => x.Email == randomNumberMail.Email);
                await _unitWork.CommitAsync();

                _unitWork.Dispose();
                return ApiResponse<bool>.Success(StatusCodes.Status200OK, result.Data);
            }


        }

        public async Task<ApiResponse<bool>> DeleteRandomNumber(RandomNumberNoVM randomNumberNoVM)
        {
            var result = new ApiResponse<bool>();

            var accountRandomNumber = await _unitWork.GetRepository<RandomNumber>().GetSingleByFilterAsync(x => x.Number == randomNumberNoVM.RandomNumberNo && x.Email == randomNumberNoVM.UserData || x.Number == randomNumberNoVM.RandomNumberNo && x.UserName == randomNumberNoVM.UserData);

            var lastNumber = await _context.RandomNumbers.OrderBy(x => x.Id).LastOrDefaultAsync(x => x.Email == accountRandomNumber.Email);
            if (lastNumber.Number != randomNumberNoVM.RandomNumberNo)
                throw new Exception($"Hatalı kod.");
            else
            {
                _unitWork.GetRepository<RandomNumber>().DeleteAllItems(x => x.Email == accountRandomNumber.Email);
                await _unitWork.CommitAsync();

                _unitWork.Dispose();

                return ApiResponse<bool>.Success(StatusCodes.Status200OK, result.Data);
            }
        }

        private int RandomNumberMetot()
        {
            System.Random rnd = new System.Random();
            int randomNumber = rnd.Next(102030, 969849);

            return randomNumber;
        }
    }
}