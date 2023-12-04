using Microsoft.AspNetCore.Mvc;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.Company;

namespace SASSTS.WebAPI.Controllers
{
    [Route("companies")]
    [ApiController]
    public class CompanyController : BaseController
    {
        private readonly ICompanyBs _companyBs;
        public CompanyController(ICompanyBs companyBs) => _companyBs = companyBs;

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllCompanies() => SendResponse(await _companyBs.GetAllAsync());

        [HttpGet("getbyId")]
        public async Task<IActionResult> GetById([FromQuery] byte id) => SendResponse(await _companyBs.GetByIdAsync(id));

        [HttpGet("getCompanyNo")]
        public async Task<IActionResult> GetByCompanyNo(string companyNo) => SendResponse(await _companyBs.GetByCompNoAsync(companyNo));

        [HttpPost("insertCompany")]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyPostDto dto) => SendResponse(await _companyBs.InsertCompany(dto));

        [HttpDelete("deleteCompany")]
        public async Task<IActionResult> Delete([FromQuery] int id) => SendResponse(await _companyBs.DeleteAsync(id));

        [HttpPut("updateCompany")]
        public async Task<IActionResult> Update([FromBody] CompanyPutDto dto) => SendResponse(await _companyBs.UpdateAsync(dto));
    }
}