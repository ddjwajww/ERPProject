using Microsoft.AspNetCore.Mvc;
using SASSTS.Business.Interfaces;

namespace SASSTS.WebAPI.Controllers
{
    [Route("basketDetails")]
    [ApiController]
    public class BasketDetailController : BaseController
    {
        private readonly IBasketDetailBs _detailBs;
        public BasketDetailController(IBasketDetailBs detailBs) => _detailBs = detailBs;

        [HttpGet("getBaskets")]
        public async Task<IActionResult> GetBaskets() => SendResponse(await _detailBs.GetAllBasketDetail("Product", "Basket"));

        [HttpPost("insertBasketDetail")]
        public async Task<IActionResult> Create([FromBody] dynamic model) => SendResponse(await _detailBs.Insert(model));

        [HttpGet("getbyrequestId")]
        public async Task<IActionResult> GetbyRequestId([FromQuery] int id) => SendResponse(await _detailBs.GetAllRequestId(id, "Product", "Basket"));
    }
}