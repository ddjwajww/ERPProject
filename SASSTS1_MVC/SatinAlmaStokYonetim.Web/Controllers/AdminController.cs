using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.ApiServices;
using SatinAlmaStokYonetim.Web.Models.Authority;
using SatinAlmaStokYonetim.Web.Models.BasketDetail;
using SatinAlmaStokYonetim.Web.Models.Category;
using SatinAlmaStokYonetim.Web.Models.Company;
using SatinAlmaStokYonetim.Web.Models.Department;
using SatinAlmaStokYonetim.Web.Models.Employee;
using SatinAlmaStokYonetim.Web.Models.Response;
using SatinAlmaStokYonetim.Web.Models.Role;
using System.Data;
using System.Text.Json;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        private readonly IValidator<CategoryPostDto> _validatorCategoryPostDto;
        private readonly IValidator<RolePostDto> _validatorRolePostDto;
        private readonly IValidator<RolePutDto> _validatorRolePutDto;
        private readonly IValidator<AuthorityPostDto> _validatorAuthorityPostDto;
        private readonly IValidator<AuthorityPutDto> _validatorAuthorityPutDto;
        private readonly IValidator<CompanyPostDto> _validatorCompanyPostDto;
        private readonly IValidator<EmployeePostDto> _validatorEmployeePostDto;
        private readonly IValidator<DepartmentPostDto> _validatorDepartmentPostDto;
        private readonly IValidator<DepartmentPutDto> _validatorDepartmentPutDto;
        private readonly IValidator<CompanyPutDto> _validatorCompanyPutDto;
        public AdminController(IHttpApiService httpApiService, IValidator<CategoryPostDto> validatorCategoryPostDto, IValidator<RolePostDto> validatorRolePostDto, IValidator<RolePutDto> validatorRolePutDto, IValidator<AuthorityPostDto> validatorAuthorityPostDto, IValidator<AuthorityPutDto> validatorAuthorityPutDto, IValidator<CompanyPostDto> validatorCompanyPostDto, IValidator<EmployeePostDto> validatorEmployeePostDto, IValidator<DepartmentPostDto> validatorDepartmentPostDto, IValidator<DepartmentPutDto> validatorDepartmentPutDto, IValidator<CompanyPutDto> validatorCompanyPutDto)
        {
            _httpApiService = httpApiService;
            _validatorCategoryPostDto = validatorCategoryPostDto;
            _validatorRolePostDto = validatorRolePostDto;
            _validatorRolePutDto = validatorRolePutDto;
            _validatorAuthorityPostDto = validatorAuthorityPostDto;
            _validatorAuthorityPutDto = validatorAuthorityPutDto;
            _validatorCompanyPostDto = validatorCompanyPostDto;
            _validatorEmployeePostDto = validatorEmployeePostDto;
            _validatorDepartmentPostDto = validatorDepartmentPostDto;
            _validatorDepartmentPutDto = validatorDepartmentPutDto;
            _validatorCompanyPutDto = validatorCompanyPutDto;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartment() =>
            await _httpApiService.GetData<List<DepartmentGetDto>>("department/getallDepartments");
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryPostDto dto) =>
            await _httpApiService.PostData2<NoData, CategoryPostDto>("api/categories/insertCategory", JsonSerializer.Serialize(dto), dto, _validatorCategoryPostDto);
        [HttpGet]
        public async Task<IActionResult> GetAllRole() =>
            await _httpApiService.GetData<List<RoleGetDto>>("roles/getAllRoles");
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RolePostDto dto) =>
            await _httpApiService.PostData2<NoData, RolePostDto>("roles/insertRole", JsonSerializer.Serialize(dto), dto, _validatorRolePostDto);
        [HttpGet]
        public async Task<IActionResult> GetAllAuthority() =>
            await _httpApiService.GetData<List<AuthorityGetDto>>("authority/getAllAuthorities");
        [HttpPost]
        public async Task<IActionResult> CreateAuthority([FromBody] AuthorityPostDto dto) =>
            await _httpApiService.PostData2<NoData, AuthorityPostDto>("authority/insertAuthority", JsonSerializer.Serialize(dto), dto, _validatorAuthorityPostDto);
        [HttpGet]
        public async Task<IActionResult> GetAllCompany() =>
            await _httpApiService.GetData<List<CompanyGetDto>>("companies/getAll");
        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody]CompanyPostDto dto) =>
            await _httpApiService.PostData2<NoData, CompanyPostDto>("companies/insertCompany", JsonSerializer.Serialize(dto), dto, _validatorCompanyPostDto);
        [HttpGet]
        public async Task<IActionResult> GetAllEmployee() =>
            await _httpApiService.GetData<List<EmployeeGetDto>>("employees/getAllEmployees");
        [HttpPost]
        public async Task<IActionResult> GetAllEmployeeByCompanyId([FromBody] int companyId) =>
             await _httpApiService.GetData<List<EmployeeGetDto>>($"employees/getallbyCompanyId?companyId={companyId}");
        [HttpPost]//Employee Delete etme işlemi burada yapılmaktadır.
        public async Task<IActionResult> DeleteEmployee([FromBody] int employeeId) =>
            await _httpApiService.DeleteData<NoData>($"employees/deleteEmployee?employeeId={employeeId}");
        [HttpPost]//Yeni kullanıcı ekleme burada account bilgileri de gönderilmelidir.
        public async Task<IActionResult> InsertEmployee([FromBody] EmployeePostDto dto) =>
            await _httpApiService.PostData2<NoData, EmployeePostDto>("employees/employeeandaccountInsert", JsonSerializer.Serialize(dto), dto, _validatorEmployeePostDto);
        [HttpPost]
        public async Task<IActionResult> InsertDepartment([FromBody] DepartmentPostDto dto) =>
             await _httpApiService.PostData2<NoData, DepartmentPostDto>("department/insertDepartment", JsonSerializer.Serialize(dto), dto, _validatorDepartmentPostDto);

        [HttpPost]
        public async Task<IActionResult> InsertRole([FromBody] RolePostDto dto) =>
        await _httpApiService.PostData2<NoData, RolePostDto>("roles/insertRole", JsonSerializer.Serialize(dto), dto, _validatorRolePostDto);

        [HttpPost]
        public async Task<IActionResult> UpdateRole([FromBody] RolePutDto dto) =>
        await _httpApiService.PutData2<NoData, RolePutDto>("roles/updateRole", JsonSerializer.Serialize(dto), dto, _validatorRolePutDto);


        [HttpPost]
        public async Task<IActionResult> DeleteRole([FromBody] int id) =>
           await _httpApiService.DeleteData<NoData>($"roles/deleteRole?id={id}");


        [HttpPost]
        public async Task<IActionResult> UpdateDepartment([FromBody] DepartmentPutDto dto) =>
            await _httpApiService.PutData2<NoData, DepartmentPutDto>("department/updateDepartment", JsonSerializer.Serialize(dto), dto, _validatorDepartmentPutDto);

        [HttpPost]
        public async Task<IActionResult> DeleteDepartment([FromBody] int id) =>
           await _httpApiService.DeleteData<NoData>($"department/deleteDepartment?id={id}");

        [HttpPost]
        public async Task<IActionResult> UpdateCompany([FromBody] CompanyPutDto dto) =>
           await _httpApiService.PutData2<NoData, CompanyPutDto>("companies/updateCompany", JsonSerializer.Serialize(dto), dto, _validatorCompanyPutDto);

        [HttpPost]
        public async Task<IActionResult> DeleteCompany([FromBody] int id) =>
           await _httpApiService.DeleteData<NoData>($"companies/deleteCompany?id={id}");
    }
}