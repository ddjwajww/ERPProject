using Microsoft.AspNetCore.Mvc;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.Support;

namespace SASSTS.WebAPI.Controllers
{
    [Route("support")]
    [ApiController]
    public class SupportController : BaseController
    {
        private readonly ISupportBs _supportBs;
        public SupportController(ISupportBs supportBs) => _supportBs = supportBs;

        [HttpGet("getallSupport")]
        public async Task<IActionResult> GetAllSupport() => SendResponse(await _supportBs.GetAll());


        [HttpPost("insertSupport")]
        public async Task<IActionResult> CreateSupport([FromBody] SupportPostDto dto) => 
            SendResponse(await _supportBs.InsertSupport(dto));  
    }
}