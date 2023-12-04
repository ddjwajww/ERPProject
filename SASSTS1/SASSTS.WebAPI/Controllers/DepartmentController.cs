using Microsoft.AspNetCore.Mvc;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.Department;

namespace SASSTS.WebAPI.Controllers
{
    [Route("department")]
    [ApiController]
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentBs _departmentBs;
        public DepartmentController(IDepartmentBs departmentBs) => _departmentBs = departmentBs;

        [HttpGet("getallDepartments")]
        public async Task<IActionResult> GetAll() => SendResponse(await _departmentBs.GetAllAsync());

        [HttpGet("getbyId")]
        public async Task<IActionResult> GetById([FromQuery] int departmenId) => SendResponse(await _departmentBs.GetById((byte)departmenId));

        [HttpPost("insertDepartment")]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentPostDto dto) => SendResponse(await _departmentBs.InsertDepartment(dto));

        [HttpDelete("deleteDepartment")]
        public async Task<IActionResult> Delete([FromQuery] int id) => SendResponse(await _departmentBs.DeleteAsync(id));

        [HttpPut("updateDepartment")]
        public async Task<IActionResult> Update([FromBody] DepartmentPutDto dto) => SendResponse(await _departmentBs.UpdateAsync(dto));
    }
}