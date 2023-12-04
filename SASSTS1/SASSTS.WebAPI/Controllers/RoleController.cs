using Microsoft.AspNetCore.Mvc;
using SASSTS.Business.Implementations;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.Role;

namespace SASSTS.WebAPI.Controllers
{
    [Route("roles")]
    [ApiController]
    public class RoleController : BaseController
    {
        private readonly IRoleBs _roleBs;
        public RoleController(IRoleBs roleBs) => _roleBs = roleBs;

        [HttpGet("getAllRoles")]
        public async Task<IActionResult> GetAll() => SendResponse(await _roleBs.GetAllAsync());

        [HttpPost("insertRole")]
        public async Task<IActionResult> CreateRole(RolePostDto dto) => SendResponse(await _roleBs.InsertRole(dto));

        [HttpDelete("deleteRole")]
        public async Task<IActionResult> Delete([FromQuery] int id) => SendResponse(await _roleBs.DeleteAsync(id));

        [HttpPut("updateRole")]
        public async Task<IActionResult> Update([FromBody] RolePutDto dto) => SendResponse(await _roleBs.UpdateAsync(dto));
    }
}