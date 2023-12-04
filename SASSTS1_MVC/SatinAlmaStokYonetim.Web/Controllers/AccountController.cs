using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.ApiServices;
using SatinAlmaStokYonetim.Web.Code;
using SatinAlmaStokYonetim.Web.Extensions;
using SatinAlmaStokYonetim.Web.MessageServices.Interfaces;
using SatinAlmaStokYonetim.Web.Models;
using SatinAlmaStokYonetim.Web.Models.Account;
using SatinAlmaStokYonetim.Web.Models.Authority;
using SatinAlmaStokYonetim.Web.Models.Response;
using SatinAlmaStokYonetim.Web.Models.Token;
using SatinAlmaStokYonetim.Web.Services;
using System.Net;
using System.Text;
using System.Text.Json;
using RandomNumber = SatinAlmaStokYonetim.Web.Models.RandomNumber;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        private readonly IValidator<LoginVM> _validatorLoginVM;
        private readonly IValidator<RandomNumberAndEmail> _validatorLogin;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly LanguageService _languageService;
        private readonly IMessageService _messageService;
        public AccountController(IHttpApiService httpApiService, IHttpClientFactory httpClientFactory, IValidator<RandomNumberAndEmail> validatorLogin, IValidator<LoginVM> validatorLoginVM, LanguageService languageService, IMessageService messageService)
        {
            _httpApiService = httpApiService;
            _httpClientFactory = httpClientFactory;
            _validatorLogin = validatorLogin;
            _validatorLoginVM = validatorLoginVM;
            _languageService = languageService;
            _messageService = messageService;
        }
        [HttpGet]
        public IActionResult Login() => View();

        [HttpGet]
        public IActionResult Register() => View();

        [HttpGet]
        public IActionResult RecoverPassword() => View();

        [HttpGet]
        public IActionResult ConfirmMail() => View();

        [HttpGet]
        public IActionResult LockScreen() => View();

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Log2([FromBody] LoginVM models)
        {

            ValidationResult result = await _validatorLoginVM.ValidateAsync(models);
            if (result.IsValid)
            {

                var newModels = new LoginVM
                {
                    UserData = models.UserData,
                    UserPassword = models.UserPassword,
                    SubjectMessageLogin = _messageService.SubjectMessageLogin(),
                    Hitap = _messageService.Hitap(),
                    LoginCodeMessage = _messageService.LoginCodeMessage(),
                };

                using HttpClient client = new HttpClient();

                client.BaseAddress = new Uri("http://localhost:5208");

                string uri = $"{client.BaseAddress}api/accountings/loginTwoFactor";

                var json = JsonSerializer.Serialize(newModels);

                var httpResponse = await client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));

                var contentData = await httpResponse.Content.ReadAsStringAsync();

                var jsonData = JsonSerializer.Deserialize<ResponseBody<RandomNumber>>(contentData,
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
        public async Task<IActionResult> Login([FromBody] RandomNumberAndEmail ldata)
        {
            ValidationResult result = await _validatorLogin.ValidateAsync(ldata);
            if (result.IsValid)
            {

                using HttpClient client = new HttpClient();

                client.BaseAddress = new Uri("http://localhost:5208/");

                string uri = $"{client.BaseAddress}api/accountings/randomNumberLogin";

                var json = JsonSerializer.Serialize(ldata);

                var httpResponse = await client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));

                var httpResponseMessage2 = await client.GetAsync($"{client.BaseAddress}authority/getAllAuthorities");

                var contentData = await httpResponse.Content.ReadAsStringAsync();

                var jsonData = JsonSerializer.Deserialize<ResponseBody<AccessTokenItem>>(contentData,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var cd = await httpResponseMessage2.Content.ReadAsStringAsync();

                var js = JsonSerializer.Deserialize<ResponseBody<List<AuthorityItem>>>(cd,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (jsonData.data != null)
                {
                    Repo.Session.Token = jsonData.data.Token;
                    Repo.Session.Name = jsonData!.data.Claims[0];
                    Repo.Session.Surname = jsonData!.data.Claims[1];
                    Repo.Session.Authority = jsonData.data.Claims[2];
                    Repo.Session.UserId = jsonData.data.Claims[3].ToString();
                    Repo.Session.CompanyId = jsonData.data.Claims[4].ToString();
                    Repo.Session.DepartmentId = jsonData.data.Claims[5].ToString();
                    Repo.Session.CompanyName = jsonData.data.Claims[6].ToString();
                    Repo.Session.DepartmentName = jsonData.data.Claims[7].ToString();
                    Repo.Session.Image = jsonData.data.Claims[8].ToString();
                    Repo.Session.AuthorityID = jsonData.data.Claims[9].ToString();
                    Repo.Session.Role = jsonData.data.Claims[10].ToString();

                    if (Repo.Session.Role == "Admin")
                    {
                        return Json(new { result = "Redirect", url = Url.Action("Home", "Admin") });
                    }
                    else if (Repo.Session.Role == "Personel")
                    {
                        return Json(new { result = "Redirect", url = Url.Action("Home", "Employee") });
                    }
                }
                else if (jsonData.errorMessages != null)
                {
                    var errorMessages = jsonData.errorMessages;
                    ValidationFailure errorMessagesV = new ValidationFailure();
                    for (int i = 0; i < errorMessages.Count; i++)
                    {
                        errorMessagesV = new ValidationFailure { PropertyName = "RandomNumber", ErrorMessage = _languageService.Getkey(errorMessages[i]).Value };
                    }

                    result.Errors.Add(errorMessagesV);
                    result.AddToModelState(ModelState);
                    return Json(new { isSuccess = false, result.Errors });
                }
                return Json(new { isSuccess = true });

            }
            else
            {
                result.AddToModelState(ModelState);


                return Json(new { isSuccess = false, result.Errors });
            }

        }

        [HttpPost]
        public async Task<IActionResult> DeleteRandomNumberLogin([FromBody] RandomNumberAndEmail ndata) =>
            await _httpApiService.PostData<NoData>("api/accountings/DeleteRandomNumberLogin", JsonSerializer.Serialize(ndata));

        [HttpGet]
        public IActionResult Logout()
        {
            Repo.Session.Name = null;
            Repo.Session.Token = null;
            Repo.Session.Authority = null;
            Repo.Session.UserId = null;
            Repo.Session.Company = null;
            Repo.Session.CompanyId = null;
            Repo.Session.Surname = null;
            Repo.Session.Image = null;
            return View();
        }
    }
}