using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNet.SignalR.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SatinAlmaStokYonetim.Web.Extensions;
using SatinAlmaStokYonetim.Web.Models.Response;
using SatinAlmaStokYonetim.Web.Models.Token;
using System.Net;
using System.Text;
using System.Text.Json;

namespace SatinAlmaStokYonetim.Web.ApiServices
{
    public class HttpApiService : Controller, IHttpApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HttpApiService(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        public async Task<IActionResult> GetData<T>(string endPoint)
        {
            var baseAddress = "http://localhost:5208/";

            var client = _httpClientFactory.CreateClient();

            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{baseAddress}{endPoint}"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json" }
                }
            };
            var responseMessage = await client.SendAsync(requestMessage);
            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<ResponseBody<T>>(jsonResponse,
               new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return Json(response!.data);
        }

        public async Task<IActionResult> PostData<T>(string endPoint, string item)
        {
            var baseAddress = "http://localhost:5208/";

            var client = _httpClientFactory.CreateClient();

            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{baseAddress}{endPoint}"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json" }
                },
                Content = new StringContent(item, Encoding.UTF8, "application/json")
            };

            var responseMessage = await client.SendAsync(requestMessage);

            return Json(new { isSuccess = true });
        }
        public async Task<IActionResult> PostData2<T, M>(string endPoint, string item, M models, IValidator<M> _validator)
        {
            ValidationResult result = await _validator.ValidateAsync(models);
            if (result.IsValid)
            {
                var baseAddress = "http://localhost:5208/";

                var client = _httpClientFactory.CreateClient();

                var requestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"{baseAddress}{endPoint}"),
                    Headers =
                {
                    { HeaderNames.Accept, "application/json" }
                },
                    Content = new StringContent(item, Encoding.UTF8, "application/json")
                };

                var responseMessage = await client.SendAsync(requestMessage);

                return Json(new { isSuccess = true });
            }
            else
            {
                result.AddToModelState(this.ModelState);

                return Json(new { isSuccess = false, result.Errors });
            }
        }

        public async Task<IActionResult> DeleteData<T>(string endPoint)
        {
            var baseAddress = "http://localhost:5208/";

            var client = _httpClientFactory.CreateClient();

            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{baseAddress}{endPoint}"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json" }
                }
            };

            var responseMessage = await client.SendAsync(requestMessage);

            return Json(new { isSuccess = true });
        }

        public async Task<IActionResult> PutData<T>(string endPoint, string item)
        {
            var baseAddress = "http://localhost:5208/";

            var client = _httpClientFactory.CreateClient();

            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{baseAddress}{endPoint}"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json" }
                },
                Content = new StringContent(item, Encoding.UTF8, "application/json")
            };

            var responseMessage = await client.SendAsync(requestMessage);

            return Json(new { isSuccess = true });
        }

        public async Task<IActionResult> PutData2<T, M>(string endPoint, string item, M models, IValidator<M> _validator)
        {
            ValidationResult result = await _validator.ValidateAsync(models);
            if (result.IsValid)
            {
                var baseAddress = "http://localhost:5208/";

                var client = _httpClientFactory.CreateClient();

                var requestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri($"{baseAddress}{endPoint}"),
                    Headers =
                {
                    { HeaderNames.Accept, "application/json" }
                },
                    Content = new StringContent(item, Encoding.UTF8, "application/json")
                };

                var responseMessage = await client.SendAsync(requestMessage);

                return Json(new { isSuccess = true });
            }
            else
            {
                result.AddToModelState(this.ModelState);

                return Json(new
                {
                    isSuccess = false,
                    result.Errors
                });
            }
        }
    }
}