using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.ApiServices;
using SatinAlmaStokYonetim.Web.Models.Response;
using SatinAlmaStokYonetim.Web.Models.Role;
using System.Text.Json;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        private readonly IValidator<RolePostDto> _validatorRolePostDto;
        private readonly IValidator<RolePutDto> _validatorRolePutDto;
        public RoleController(IHttpApiService httpApiService, IValidator<RolePostDto> validatorRolePostDto, IValidator<RolePutDto> validatorRolePutDto)
        {
            _httpApiService = httpApiService;
            _validatorRolePostDto = validatorRolePostDto;
            _validatorRolePutDto = validatorRolePutDto;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRole() =>
            await _httpApiService.GetData<List<RoleGetDto>>("roles/getAllRoles");

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RolePostDto dto) =>
            await _httpApiService.PostData2<NoData, RolePostDto>("roles/insertRole", JsonSerializer.Serialize(dto), dto, _validatorRolePostDto);

        [HttpPost]
        public async Task<IActionResult> InsertRole([FromBody] RolePostDto dto) =>
            await _httpApiService.PostData2<NoData, RolePostDto>("roles/insertRole", JsonSerializer.Serialize(dto), dto, _validatorRolePostDto);

        [HttpPost]
        public async Task<IActionResult> UpdateRole([FromBody] RolePutDto dto) =>
            await _httpApiService.PutData2<NoData, RolePutDto>("roles/updateRole", JsonSerializer.Serialize(dto), dto, _validatorRolePutDto);

        [HttpPost]
        public async Task<IActionResult> DeleteRole([FromBody] int id) =>
           await _httpApiService.DeleteData<NoData>($"roles/deleteRole?id={id}");
    }
}
