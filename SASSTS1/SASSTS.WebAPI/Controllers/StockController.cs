using Microsoft.AspNetCore.Mvc;
using SASSTS.Business.Interfaces;

namespace SASSTS.WebAPI.Controllers
{
    [Route("stock")]
    [ApiController]
    public class StockController : BaseController
    {
        private readonly IStockBs _stockBs;
        public StockController(IStockBs stockBs) => _stockBs = stockBs;

        [HttpGet("getallstockbyCompanyId")]//CompanyId değerine göre filtreleme yaparak tüm stokları görüntüler.
        public async Task<IActionResult> GetAllStock([FromQuery] int companyId) => SendResponse(await _stockBs.GetallStockbyCompanyIdAsync(companyId, "Product", "Company"));
    }
}