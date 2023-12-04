using AutoMapper;
using Infrastructure.ApiResponses;
using Infrastructure.Mail.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SASSTS.Business.Controls.Dto;
using SASSTS.Business.Interfaces;
using SASSTS.Business.Security.Password;
using SASSTS.DataAccess.UnitOfWork;
using SASSTS.Model.Dtos.Accounting;
using SASSTS.Model.Dtos.Employee;
using SASSTS.Model.Entities;
using SASSTS.Model.RequestModels.AccountingVM;

namespace SASSTS.Business.Implementations
{
    public class EmployeeBs : BusinessMap, IEmployeeBs
    {
        private readonly IExceptionValidDto<Employee, EmployeeGetDto, EmployeeBs, EmployeePostDto, EmployeePutDto> _emp;
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeBs> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        public EmployeeBs(IUnitWork unitWork, IMapper mapper, ILogger<EmployeeBs> logger, IExceptionValidDto<Employee, EmployeeGetDto, EmployeeBs, EmployeePostDto, EmployeePutDto> emp, IConfiguration configuration, IMailService mailService)
        {
            _unitWork = unitWork; _mapper = mapper; _logger = logger; _emp = emp; _configuration = configuration;
            _mailService = mailService;
        }
        public async Task<ApiResponse<List<EmployeeGetDto>>> GetAllAsync() => await _emp.GetAllDtosController("Employee", prd => prd.IsActive == true);
        public async Task<ApiResponse<List<EmployeeGetDto>>> GetallbyCompanyId(int companyId, params string[] includeList) => await _emp
            .GetAllDtosController("Employee", prd => prd.CompanyId == companyId && prd.IsActive == true, includeList: includeList);
        public async Task<ApiResponse<UpdateUserVM>> GetbyId(int employeeId, string include) =>
            await ExceptionValidDto<Accounting, UpdateUserVM, EmployeeBs>.GetbyFilter(unitWork: _unitWork, table: "Employee", mapper: _mapper, logger: _logger,
                predicate: x => x.Id == employeeId, include: include);
        public async Task<ApiResponse<NoData>> Delete(int employeeId) => await _emp.DeleteAsync("Employee", prd => prd.Id == employeeId, mdl => mdl.IsActive = false);
        public async Task<ApiResponse<NoData>> UpdateAsync(EmployeePutDto dto)
        {
            var model = _mapper.Map<Employee>(dto);
            model.IsActive = true;
            await _unitWork.GetRepository<Employee>().UpdateAsync(model);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<NoData>> CreateEmployee(EmployeePostDto dto)
        {
            var identityNumberExists = await _unitWork.GetRepository<Employee>().AnyAsync(x => x.IdentityNo.Trim() == dto.IdentityNo.Trim(), "Employee");
            if (identityNumberExists)
                return ApiResponse<NoData>.Fail(StatusCodes.Status401Unauthorized, "Girmiş olduğunuz TC kimlik numarası kayıtlı.");

            var phoneExists = await _unitWork.GetRepository<Employee>().AnyAsync(x => x.Phone.Trim() == dto.Phone.Trim(), "Employee");
            if (phoneExists)
                return ApiResponse<NoData>.Fail(StatusCodes.Status401Unauthorized, "Girmiş olduğunuz telefon numarası kayıtlı.");

            var emailExists = await _unitWork.GetRepository<Employee>().AnyAsync(x => x.Email.Trim() == dto.Email.Trim(), "Employee");
            if (emailExists)
                return ApiResponse<NoData>.Fail(StatusCodes.Status401Unauthorized, "ePosta adresi kullanılmaktadır. Lütfen farklı bir eposta adresi giriniz.");

            var customerEntity = _mapper.Map<Employee>(dto);

            var accountEntity = _mapper.Map<Accounting>(dto);

            accountEntity.UserPassword = CipherUtil
                .EncryptString(_configuration["AppSettings:SecretKey"], accountEntity.UserPassword);

            accountEntity.Employee = customerEntity;

            _unitWork.GetRepository<Employee>().Add(customerEntity);
            _unitWork.GetRepository<Accounting>().Add(accountEntity);
            await _unitWork.CommitAsync();

            var bodyMessage = dto.Hitap + " " + dto.EmployeeName + " " + dto.EmployeeSurname.ToUpper() + " " + dto.RegisterMessage + " " + dto.UserNameMessage + " " + dto.UserName + " " + dto.PasswordMessage + " " + dto.UserPassword + " " + dto.PasswordResetMessage;
            await _mailService.SendMessageAsync($"{dto.Email}", dto.SubjectMessage, bodyMessage);

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }


        public async Task<ApiResponse<NoData>> Update222(EAMPUTDTO dto)
        {
            var account = await _unitWork.GetRepository<Accounting>().GetAsync(x => x.EmployeeId == dto.Id);
            account.Email = dto.Email;
            account.UserName = dto.UserName;
            var hashedPassword = CipherUtil.EncryptString(_configuration["AppSettings:SecretKey"], dto.UserPassword);
            account.UserPassword = hashedPassword;
            await _unitWork.GetRepository<Accounting>().UpdateAsync(account);

            var employee = await _unitWork.GetRepository<Employee>().GetAsync(x => x.Id == account.EmployeeId);
            employee.Email = dto.Email;
            employee.Phone = dto.Phone;
            employee.Image = dto.Image;
            await _unitWork.GetRepository<Employee>().UpdateAsync(employee);

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<List<EmployeeGetUIDto>>> GetAllEmployeebyJoin(params string[] includeList)
        {
            var employees = await _unitWork.GetRepository<Employee>().GetAllAsync(includeList: includeList);
            var returnList = _mapper.Map<List<EmployeeGetUIDto>>(employees);
            return ApiResponse<List<EmployeeGetUIDto>>.Success(StatusCodes.Status200OK, returnList);
        }

    }
}