using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.ApiServices;
using SatinAlmaStokYonetim.Web.Models.Response;
using SatinAlmaStokYonetim.Web.Models.Stock;
using SatinAlmaStokYonetim.Web.Models.StockOperation;
using System.Text.Json;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class StockProcessController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        public StockProcessController(IHttpApiService httpApiService) => _httpApiService = httpApiService;

        [HttpPost] //Stok getir!.
        public async Task<IActionResult> GetallStockbyCompanyId([FromBody] int companyId) =>
            await _httpApiService.GetData<List<StockItem>>($"stock/getallstockbyCompanyId?companyId={companyId}");

        [HttpPost] //Stok operation tablosundan get etkemk için buraya bir stok ıd değeri gerekli!!!!.
        public async Task<IActionResult> GetallStockOperationbyStockId([FromBody] int stockId) =>
            await _httpApiService.GetData<List<StockOperationItem>>($"stockOperation/getallOperationbystockId?stockId={stockId}");

        [HttpPost] //Stock Çıkış işlemleri için bu kontroller kullanılacak...
        public async Task<IActionResult> LogOutStock([FromBody] StockOperationOutItem item) =>
            await _httpApiService.PostData<NoData>("stockOperation/logoutstock", JsonSerializer.Serialize(item));
    }
}