using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.ApiServices;
using SatinAlmaStokYonetim.Web.Models.Company;
using SatinAlmaStokYonetim.Web.Models.Response;
using System.Text.Json;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        private readonly IValidator<CompanyPutDto> _validatorCompanyPutDto;
        private readonly IValidator<CompanyPostDto> _validatorCompanyPostDto;
        public CompanyController(IHttpApiService httpApiService, IValidator<CompanyPutDto> validatorCompanyPutDto, IValidator<CompanyPostDto> validatorCompanyPostDto)
        {
            _httpApiService = httpApiService;
            _validatorCompanyPostDto = validatorCompanyPostDto;
            _validatorCompanyPutDto = validatorCompanyPutDto;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompany() =>
           await _httpApiService.GetData<List<CompanyGetDto>>("companies/getAll");


        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyPostDto dto) =>
            await _httpApiService.PostData2<NoData, CompanyPostDto>("companies/insertCompany", JsonSerializer.Serialize(dto), dto, _validatorCompanyPostDto);

        [HttpPost]
        public async Task<IActionResult> UpdateCompany([FromBody] CompanyPutDto dto) =>
           await _httpApiService.PutData2<NoData, CompanyPutDto>("companies/updateCompany", JsonSerializer.Serialize(dto), dto, _validatorCompanyPutDto);

        [HttpPost]
        public async Task<IActionResult> DeleteCompany([FromBody] int id) =>
           await _httpApiService.DeleteData<NoData>($"companies/deleteCompany?id={id}");
    }
}
