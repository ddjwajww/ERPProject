using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.ApiServices;
using SatinAlmaStokYonetim.Web.Models.Authority;
using SatinAlmaStokYonetim.Web.Models.Category;
using SatinAlmaStokYonetim.Web.Models.Company;
using SatinAlmaStokYonetim.Web.Models.Department;
using SatinAlmaStokYonetim.Web.Models.Employee;
using SatinAlmaStokYonetim.Web.Models.Response;
using SatinAlmaStokYonetim.Web.Models.Role;
using System.Text.Json;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        private readonly IValidator<DepartmentPostDto> _validatorDepartmentPostDto;
        private readonly IValidator<DepartmentPutDto> _validatorDepartmentPutDto;
        public DepartmentController(IHttpApiService httpApiService, IValidator<DepartmentPostDto> validatorDepartmentPostDto,  IValidator<DepartmentPutDto> validatorDepartmentPutDto)
        {
            _httpApiService = httpApiService;
            _validatorDepartmentPostDto = validatorDepartmentPostDto;
            _validatorDepartmentPutDto = validatorDepartmentPutDto;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllDepartment() =>
            await _httpApiService.GetData<List<DepartmentGetDto>>("department/getallDepartments");

        [HttpPost]
        public async Task<IActionResult> InsertDepartment([FromBody] DepartmentPostDto dto) =>
            await _httpApiService.PostData2<NoData, DepartmentPostDto>("department/insertDepartment", JsonSerializer.Serialize(dto), dto, _validatorDepartmentPostDto);

        [HttpPost]
        public async Task<IActionResult> UpdateDepartment([FromBody] DepartmentPutDto dto) =>
            await _httpApiService.PutData2<NoData, DepartmentPutDto>("department/updateDepartment", JsonSerializer.Serialize(dto), dto, _validatorDepartmentPutDto);

        [HttpPost]
        public async Task<IActionResult> DeleteDepartment([FromBody] int id) =>
            await _httpApiService.DeleteData<NoData>($"department/deleteDepartment?id={id}");

    }
}
