using Microsoft.AspNetCore.Mvc;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.Supplier;

namespace SASSTS.WebAPI.Controllers
{
    [Route("suppliers")]
    [ApiController]
    public class SupplierController : BaseController
    {
        private readonly ISupplierBs _supplierBs;
        public SupplierController(ISupplierBs supplierBs) => _supplierBs = supplierBs;

        [HttpGet("getallSupplier")]
        public async Task<IActionResult> GetAll() => SendResponse(await _supplierBs.GetAllAsync());

        [HttpPost("insertSupplier")]
        public async Task<IActionResult> CreateSupplier([FromBody] SupplierPostDto dto) => SendResponse(await _supplierBs.Insert(dto));

        [HttpPut("updateSupplier")]
        public async Task<IActionResult> Update([FromBody] SupplierPutDto dto) => SendResponse(await _supplierBs.UpdateAsync(dto));
    }
}