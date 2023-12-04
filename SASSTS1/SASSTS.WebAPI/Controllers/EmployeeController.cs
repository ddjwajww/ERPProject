using Microsoft.AspNetCore.Mvc;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.Accounting;
using SASSTS.Model.Dtos.Employee;

namespace SASSTS.WebAPI.Controllers
{
    [Route("employees")]
    [ApiController]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeBs _employeeBs;
        public EmployeeController(IEmployeeBs employeeBs) => _employeeBs = employeeBs;

        [HttpGet("getbyemployeeId")]
        public async Task<IActionResult> GetbyEmployeeId([FromQuery] int employeeId) => SendResponse(await _employeeBs.GetbyId(employeeId, "Employee"));

        [HttpGet("getAllEmployees")]
        public async Task<IActionResult> GetAll() => SendResponse(await _employeeBs.GetAllAsync());

        [HttpGet("getallbyCompanyId")]
        public async Task<IActionResult> GetallbyCompanyId([FromQuery] int companyId) => SendResponse(await _employeeBs.GetallbyCompanyId(companyId));

        [HttpPost("employeeandaccountInsert")]
        public async Task<IActionResult> Insert([FromBody]EmployeePostDto dto) => SendResponse(await _employeeBs.CreateEmployee(dto));

        [HttpDelete("deleteEmployee")]
        public async Task<IActionResult> DeleteEmployee([FromQuery] int employeeId) => SendResponse(await _employeeBs.Delete(employeeId));

        [HttpPut("updateEmployee")]
        public async Task<IActionResult> Update([FromBody] EmployeePutDto dto) => SendResponse(await _employeeBs.UpdateAsync(dto));

        [HttpPut("updateemployeeandAccount")]
        public async Task<IActionResult> Update2([FromBody] EAMPUTDTO dto) => SendResponse(await _employeeBs.Update222(dto));

        [HttpGet("getallemployeebyJoin")]
        public async Task<IActionResult> GetAllEmployeebyJoin() => SendResponse(await _employeeBs.GetAllEmployeebyJoin("Role", "Authority", "Department", "Company"));    
    }
}