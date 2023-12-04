using Infrastructure.ApiResponses;
using SASSTS.Model.Dtos.Department;
using SASSTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Business.Interfaces
{
    public interface IDepartmentBs
    {
        Task<ApiResponse<DepartmentGetDto>> GetById(byte id);
        Task<ApiResponse<List<DepartmentGetDto>>> GetAllAsync();
        Task<ApiResponse<NoData>> InsertDepartment(DepartmentPostDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
        Task<ApiResponse<NoData>> UpdateAsync(DepartmentPutDto dto);
    }
}
