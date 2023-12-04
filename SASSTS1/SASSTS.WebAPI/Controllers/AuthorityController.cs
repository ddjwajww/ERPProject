using Microsoft.AspNetCore.Mvc;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.Authority;

namespace SASSTS.WebAPI.Controllers
{
    [Route("authority")]
    [ApiController]
    public class AuthorityController : BaseController
    {
        private readonly IAuthorityBs _authorityBs;
        public AuthorityController(IAuthorityBs authorityBs) => _authorityBs = authorityBs;

        [HttpGet("getAllAuthorities")]
        public async Task<IActionResult> GetAll() => SendResponse(await _authorityBs.GetAllAsync());

        [HttpPost("insertAuthority")]
        public async Task<IActionResult> CreateAuthority([FromBody] AuthorityPostDto dto) => SendResponse(await _authorityBs.InsertAuthority(dto));

        [HttpDelete("deleteAuthority")]
        public async Task<IActionResult> Delete([FromQuery] int id) => SendResponse(await _authorityBs.DeleteAsync(id));

        [HttpPut("updateAuthority")]
        public async Task<IActionResult> Update([FromBody] AuthorityPutDto dto) => SendResponse(await _authorityBs.UpdateAsync(dto));
    }
}