using Infrastructure.ApiResponses;
using Infrastructure.Models;
using SASSTS.Business.Controls.Dto;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.Role;
using SASSTS.Model.Entities;

namespace SASSTS.Business.Implementations
{
    public class RoleBs : BusinessMap ,IRoleBs
    {
        private readonly IExceptionValidDto<Role, RoleGetDto, RoleBs, RolePostDto, RolePutDto> _role;
        public RoleBs(IExceptionValidDto<Role, RoleGetDto, RoleBs, RolePostDto, RolePutDto> role) => _role = role;
        public async Task<ApiResponse<List<RoleGetDto>>> GetAllAsync() => await _role.GetAllDtosController("Role", x => x.IsDeleted == false);
        public async Task<ApiResponse<NoData>> InsertRole(RolePostDto dto) => await _role.InsertAsyncNoData(dto, "Role", x => x.IsDeleted = false,
            x => x.RoleName == dto.RoleName);
        public async Task<ApiResponse<NoData>> DeleteAsync(int id) => await _role.DeleteAsync("Role", x => x.Id == id, m => m.IsDeleted = true);
        public async Task<ApiResponse<NoData>> UpdateAsync(RolePutDto dto) => await _role.UpdateDto(dto, "Role", null!, x => x.RoleName == dto.RoleName);
    }
}