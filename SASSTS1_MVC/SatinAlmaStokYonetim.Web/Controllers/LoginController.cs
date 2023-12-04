using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.ApiServices;
using SatinAlmaStokYonetim.Web.Extensions;
using SatinAlmaStokYonetim.Web.MessageServices.Interfaces;
using SatinAlmaStokYonetim.Web.Models.Account;
using SatinAlmaStokYonetim.Web.Models.Response;
using SatinAlmaStokYonetim.Web.Services;
using System.Net;
using System.Text;
using System.Text.Json;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        private readonly IValidator<EmployeeMail> _validatorMail;
        private readonly IValidator<RandomNumberAndEmail> _validatorRandomNumberAndEmail;
        private readonly IValidator<NewPassword> _validatorNewPassword;
        private readonly LanguageService _languageService;
        private readonly IMessageService _messageService;
        public LoginController(IHttpApiService httpApiService, IValidator<EmployeeMail> validatorMail, IValidator<RandomNumberAndEmail> validatorRandomNumberAndEmail, IValidator<NewPassword> validatorNewPassword, LanguageService languageService, IMessageService messageService)
        {
            _httpApiService = httpApiService;
            _validatorMail = validatorMail;
            _validatorRandomNumberAndEmail = validatorRandomNumberAndEmail;
            _validatorNewPassword = validatorNewPassword;
            _languageService = languageService;
            _messageService = messageService;
        }

        [HttpPost]
        public async Task<IActionResult> ForgottenPassword([FromBody] EmployeeMail mail)
        {
            ValidationResult result = await _validatorMail.ValidateAsync(mail);
            if (result.IsValid)
            {

                var newMail = new EmployeeMail
                {
                    UserData = mail.UserData,
                    SubjectMessagePassword = _messageService.SubjectMessagePassword(),
                    Hitap = _messageService.Hitap(),
                    LoginCodeMessage = _messageService.LoginCodeMessage(),
                };
                using HttpClient client = new HttpClient();

                client.BaseAddress = new Uri("http://localhost:5208/");

                string uri = $"{client.BaseAddress}api/accountings/forgottenPassword";

                var json = JsonSerializer.Serialize(newMail);

                var httpResponse = await client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));

                var contentData = await httpResponse.Content.ReadAsStringAsync();

                var jsonData = JsonSerializer.Deserialize<ResponseBody<int>>(contentData,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (httpResponse.StatusCode == HttpStatusCode.OK && jsonData.errorMessages == null)
                {
                    return Json(new { isSuccess = true, jsonData.data });
                }
                else
                {
                    var errorMessages = jsonData.errorMessages;
                    ValidationFailure errorMessagesV = new ValidationFailure();
                    for (int i = 0; i < errorMessages.Count; i++)
                    {
                        errorMessagesV = new ValidationFailure { PropertyName = "ApiErrors", ErrorMessage = errorMessages[i] };
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
        public async Task<IActionResult> RandomNumberConfirm([FromBody] RandomNumberAndEmail code)
        {
            ValidationResult result = await _validatorRandomNumberAndEmail.ValidateAsync(code);
            if (result.IsValid)
            {
                using HttpClient client = new HttpClient();

                client.BaseAddress = new Uri("http://localhost:5208/api/accountings");

                string uri = $"{client.BaseAddress}/RandomNumberConfirm";

                var json = JsonSerializer.Serialize(code);

                var httpResponseMessage = await client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));

                var contentData = await httpResponseMessage.Content.ReadAsStringAsync();

                var jsonData = JsonSerializer.Deserialize<ResponseBody<long>>(contentData,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (httpResponseMessage.StatusCode == HttpStatusCode.OK && jsonData.errorMessages == null)
                {
                    return Json(new { isSuccess = true, jsonData });
                }
                else
                {
                    var errorMessages = jsonData.errorMessages;
                    ValidationFailure errorMessagesV = new ValidationFailure();
                    for (int i = 0; i < errorMessages.Count; i++)
                    {
                        errorMessagesV = new ValidationFailure { PropertyName = "ApiErrors", ErrorMessage = errorMessages[i] };
                    }

                    result.Errors.Add(errorMessagesV);
                    result.AddToModelState(ModelState);
                    return Json(new { isSuccess = false, result.Errors });
                }

            }
            else
            {
                result.AddToModelState(this.ModelState);


                return Json(new { result.Errors });
            }
        }


        [HttpPost]
        public async Task<IActionResult> NewPassword([FromBody] NewPassword newpass)
        {
            ValidationResult result = await _validatorNewPassword.ValidateAsync(newpass);
            if (result.IsValid)
            {

                var newPassword = new NewPassword
                {
                    Id = newpass.Id,
                    UserPassword = newpass.UserPassword,
                    RandomNumberNo = newpass.RandomNumberNo,
                    SubjectMessagePassword = _messageService.SubjectMessagePassword(),
                    Hitap = _messageService.Hitap(),
                    NewPasswordMessage = _messageService.NewPasswordMessage(),
                };

                using HttpClient client = new HttpClient();

                client.BaseAddress = new Uri("http://localhost:5208/api/accountings");

                string uri = $"{client.BaseAddress}/NewPassword";

                var json = JsonSerializer.Serialize(newPassword);

                var httpResponseMessage = await client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));

                var contentData = await httpResponseMessage.Content.ReadAsStringAsync();

                var jsonData = JsonSerializer.Deserialize<ResponseBody<bool>>(contentData,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (httpResponseMessage.StatusCode == HttpStatusCode.OK && jsonData.errorMessages == null)
                {
                    return Json(new { isSuccess = true });
                }
                else
                {
                    var errorMessages = jsonData.errorMessages;
                    ValidationFailure errorMessagesV = new ValidationFailure();
                    for (int i = 0; i < errorMessages.Count; i++)
                    {
                        errorMessagesV = new ValidationFailure { PropertyName = "ApiErrors", ErrorMessage = errorMessages[i] };
                    }

                    result.Errors.Add(errorMessagesV);
                    result.AddToModelState(ModelState);
                    return Json(new { isSuccess = false, result.Errors });
                }
            }
            else
            {
                result.AddToModelState(this.ModelState);


                return Json(new { result.Errors });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRandomNumber([FromBody] NewPassword newpass) =>
            await _httpApiService.PostData<NoData>("api/accountings/DeleteRandomNumber", JsonSerializer.Serialize(newpass));

    }
}
