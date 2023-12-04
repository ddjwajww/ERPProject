using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SatinAlmaStokYonetim.Web.ApiServices;
using SatinAlmaStokYonetim.Web.Code;
using SatinAlmaStokYonetim.Web.Extensions;
using SatinAlmaStokYonetim.Web.MessageServices.Interfaces;
using SatinAlmaStokYonetim.Web.Models;
using SatinAlmaStokYonetim.Web.Models.Account;
using SatinAlmaStokYonetim.Web.Models.Employee;
using SatinAlmaStokYonetim.Web.Models.Response;
using SatinAlmaStokYonetim.Web.Services;
using System.ComponentModel.Design;
using System.Net;
using System.Net.Http;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        private readonly IValidator<EmployeePostDto> _validatorEmployeePostDto;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMessageService _messageService;
        private readonly LanguageService _languageService;

        public EmployeeController(IHttpApiService httpApiService, IValidator<EmployeePostDto> validatorEmployeePostDto, IHttpClientFactory httpClientFactory, IMessageService messageService, LanguageService languageService)
        {
            _httpApiService = httpApiService;
            _validatorEmployeePostDto = validatorEmployeePostDto;
            _httpClientFactory = httpClientFactory;
            _messageService = messageService;
            _languageService = languageService;
        }

        [HttpPost]
        public async Task<IActionResult> GetbyEmployeeId([FromBody] int employeeId) =>
            await _httpApiService.GetData<EmployeeGetDto>($"employees/getbyemployeeId?employeeId={employeeId}");

        [HttpGet]
        public async Task<IActionResult> GetAllEmployee() =>
            await _httpApiService.GetData<List<EmployeeGetDto>>("employees/getAllEmployees");

        [HttpPost]
        public async Task<IActionResult> GetAllEmployeeByCompanyId([FromBody] int companyId) =>
             await _httpApiService.GetData<List<EmployeeGetDto>>($"employees/getallbyCompanyId?companyId={companyId}");

        [HttpPost]//Employee Delete etme işlemi burada yapılmaktadır.
        public async Task<IActionResult> DeleteEmployee([FromBody] int employeeId) =>
            await _httpApiService.DeleteData<NoData>($"employees/deleteEmployee?employeeId={employeeId}");


        [HttpPost]//Yeni kullanıcı ekleme burada account bilgileri de gönderilmelidir.
        public async Task<IActionResult> InsertEmployee([FromBody] EmployeePostDto dto)
        //await _httpApiService.PostData2<NoData, EmployeePostDto>("employees/employeeandaccountInsert", JsonSerializer.Serialize(dto), dto, _validatorEmployeePostDto);
        {
            ValidationResult result = await _validatorEmployeePostDto.ValidateAsync(dto);
            if (result.IsValid)
            {
                var userName = dto.Email.Substring(0, dto.Email.IndexOf("@"));
                var userPassword = "A.T-" + userName[0].ToString().ToUpper() + userName.Substring(1) + "1!";
                var newDto = new EmployeePostDto
                {
                    RoleId = dto.RoleId,
                    AuthorityId = dto.AuthorityId,
                    CompanyId = dto.CompanyId,
                    DepartmentId = dto.DepartmentId,
                    EmployeeName = dto.EmployeeName,
                    EmployeeSurname = dto.EmployeeSurname,
                    Phone = dto.Phone,
                    Email = dto.Email,
                    Image = dto.Image,
                    IdentityNo = dto.IdentityNo,
                    SubjectMessage = _messageService.SubjectMessage(),
                    Hitap = _messageService.Hitap(),
                    RegisterMessage = _messageService.RegisterMessage(),
                    UserNameMessage = _messageService.UserNameMessage(),
                    PasswordMessage = _messageService.PasswordMessage(),
                    PasswordResetMessage = _messageService.PasswordResetMessage(),
                    UserName = userName,
                    UserPassword = userPassword,
                };

                using HttpClient client = new HttpClient();

                client.BaseAddress = new Uri("http://localhost:5208/");

                string uri = $"{client.BaseAddress}employees/employeeandaccountInsert";

                var json = JsonSerializer.Serialize(newDto);

                var httpResponse = await client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));

                var contentData = await httpResponse.Content.ReadAsStringAsync();

                var jsonData = JsonSerializer.Deserialize<ResponseBody<NoData>>(contentData,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (httpResponse.StatusCode == HttpStatusCode.OK && jsonData.errorMessages == null)
                {
                    return Json(new { isSuccess = true });
                }
                else
                {
                    var errorMessages = jsonData.errorMessages;
                    ValidationFailure errorMessagesV = new ValidationFailure();
                    for (int i = 0; i < errorMessages.Count; i++)
                    {
                        errorMessagesV = new ValidationFailure { PropertyName = "ApiErrors", ErrorMessage = _languageService.Getkey(errorMessages[i]).Value };
                    }

                    result.Errors.Add(errorMessagesV);
                    result.AddToModelState(ModelState);
                    return Json(new { isSuccess = false, result.Errors });
                }
            }
            else
            {
                result.AddToModelState(this.ModelState);

                return Json(new { isSuccess = false, result.Errors });
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostUpdateAccount([FromBody] EAMPUTDTO dto)
        {
            Repo.Session.Image = dto.Image;
            return await _httpApiService.PutData<NoData>("employees/updateemployeeandAccount", JsonSerializer.Serialize(dto));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeePutDto dto) =>
          await _httpApiService.PutData<NoData>("employees/updateEmployee", JsonSerializer.Serialize(dto));

        [HttpPost]
        public async Task<IActionResult> GetAccountbyAccountingId([FromBody] long id) => await _httpApiService.GetData<EmployeeandAccountModel>($"api/accountings/getaccountbyAccountId?accountingId={id}");
    }
}
