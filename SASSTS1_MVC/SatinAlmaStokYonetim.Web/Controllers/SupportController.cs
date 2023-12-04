using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.ApiServices;
using SatinAlmaStokYonetim.Web.Models.Response;
using SatinAlmaStokYonetim.Web.Models.Support;
using System.Text.Json;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class SupportController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        public SupportController(IHttpApiService httpApiService) => _httpApiService = httpApiService;

        [HttpPost]
        public async Task<IActionResult> CreateSupport([FromBody] SupportPostDto dto) => 
            await _httpApiService.PostData<NoData>("support/insertSupport", JsonSerializer.Serialize(dto));  
    }
}
