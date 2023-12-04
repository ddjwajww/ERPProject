using Infrastructure.ApiResponses;
using SASSTS.Model.Dtos.Role;
using SASSTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Business.Interfaces
{
    public interface IRoleBs
    {
        Task<ApiResponse<List<RoleGetDto>>> GetAllAsync();
        Task<ApiResponse<NoData>> InsertRole(RolePostDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
        Task<ApiResponse<NoData>> UpdateAsync(RolePutDto dto);
    }
}
