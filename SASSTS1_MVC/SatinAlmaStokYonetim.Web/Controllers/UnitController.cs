using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.ApiServices;
using SatinAlmaStokYonetim.Web.Models.Unit;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class UnitController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        public UnitController(IHttpApiService httpApiService) => _httpApiService = httpApiService;
        [HttpGet]
        public async Task<IActionResult> GetAllUnit() =>
            await _httpApiService.GetData<List<UnitItem>>("api/units/getUnits");
    }
}
