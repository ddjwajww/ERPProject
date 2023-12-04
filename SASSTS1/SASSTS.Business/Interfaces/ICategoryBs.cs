using Infrastructure.ApiResponses;
using SASSTS.Model.Dtos.Category;
using SASSTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Business.Interfaces
{
    public interface ICategoryBs
    {
        Task<ApiResponse<List<CategoryGetDto>>> GetAllCategories();
        Task<ApiResponse<CategoryGetDto>> GetbyIdAsync(byte id);
        Task<ApiResponse<NoData>> InsertCategory(CategoryPostDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
        Task<ApiResponse<NoData>> UpdateAsync(CategoryPutDto dto);
    }
}
