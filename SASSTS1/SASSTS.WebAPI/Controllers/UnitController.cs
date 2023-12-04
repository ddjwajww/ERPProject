using Microsoft.AspNetCore.Mvc;
using SASSTS.Business.Implementations;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.Unit;

namespace SASSTS.WebAPI.Controllers
{
    [Route("api/units")]
    [ApiController]
    public class UnitController : BaseController
    {
        private readonly IUnitBs _unitBs;
        public UnitController(IUnitBs unitBs) => _unitBs = unitBs;

        [HttpGet("getUnits")]
        public async Task<IActionResult> GetUnits() => SendResponse(await _unitBs.GetAllUnits());

        [HttpDelete("deleteUnit")]
        public async Task<IActionResult> Delete([FromQuery] int id) => SendResponse(await _unitBs.DeleteAsync(id));

        [HttpPost("insertUnit")]
        public async Task<IActionResult> Create([FromBody] UnitPostDto dto) => SendResponse(await _unitBs.InsertUnit(dto));

        [HttpPut("updateUnit")]
        public async Task<IActionResult> Update([FromBody] UnitPutDto dto) => SendResponse(await _unitBs.UpdateAsync(dto));
    }
}