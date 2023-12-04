using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.ApiServices;
using SatinAlmaStokYonetim.Web.Models.Basket;
using SatinAlmaStokYonetim.Web.Models.BasketDetail;
using SatinAlmaStokYonetim.Web.Models.Response;
using System.Text.Json;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        public BasketController(IHttpApiService httpApiService) => _httpApiService = httpApiService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BasketModel model) =>
            await _httpApiService.PostData<NoData>("baskets/insertBasket", JsonSerializer.Serialize(model));

        [HttpPost]
        public async Task<IActionResult> GetbyBasketId([FromBody] int id) =>
            await _httpApiService.GetData<List<BasketDetailGetDto>>($"basketDetails/getbyrequestId?id={id}");
    }
}