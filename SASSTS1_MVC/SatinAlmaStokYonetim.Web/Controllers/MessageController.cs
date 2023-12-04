using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.ApiServices;
using SatinAlmaStokYonetim.Web.Models.Category;
using SatinAlmaStokYonetim.Web.Models.Message;
using SatinAlmaStokYonetim.Web.Models.Response;
using System.Text.Json;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class MessageController : Controller, IHub
    {
        private readonly IHttpApiService _httpApiService;

        public HubCallerContext Context { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IHubCallerConnectionContext<dynamic> Clients { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IGroupManager Groups { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public MessageController(IHttpApiService httpApiService) => _httpApiService = httpApiService;

        [HttpPost]
        public async Task<IActionResult> GetAllEmployeeByCompanyId([FromBody] int companyId) =>
            await _httpApiService.GetData<List<MessageAccount>>($"api/accountings/getallbyCompanyId?companyId={companyId}");

        [HttpGet]
        public async Task<IActionResult> GetAllMessages() =>
            await _httpApiService.GetData<List<GetAllMessage>>("message/getallMessage");


        public Task OnConnected()
        {
            throw new NotImplementedException();
        }

        public Task OnReconnected()
        {
            throw new NotImplementedException();
        }

        public Task OnDisconnected(bool stopCalled)
        {
            throw new NotImplementedException();
        }

        [HttpPost()]
        public async Task<IActionResult> GetAllbyAuthorityId([FromBody] int authorityId) =>
            await _httpApiService.GetData<List<GetAllMessage>>($"message/getAllbyAuthorityId?authorityId={authorityId}");

        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] MessagePostDto dto) =>
            await _httpApiService.PostData<NoData>("message/insertMessage", JsonSerializer.Serialize(dto));
    }
}