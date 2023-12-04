using Microsoft.AspNetCore.Mvc;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.StockOperation;

namespace SASSTS.WebAPI.Controllers
{
    [Route("stockOperation")]
    [ApiController]
    public class StockOperationController : BaseController
    {
        private readonly IStockOperationBs _stockOperationBs;
        public StockOperationController(IStockOperationBs stockOperationBs) => _stockOperationBs = stockOperationBs;

        [HttpGet("getallOperationbystockId")]
        public async Task<IActionResult> GetAllOperations([FromQuery] int stockId) => SendResponse(await _stockOperationBs
            .GetallOperation(stockId, "Employee"));

        [HttpPost("logoutstock")]
        public async Task<IActionResult> LogOut([FromBody] StockOperationPostDto dto) => SendResponse(await _stockOperationBs.LogOutStock(dto));
    }
}