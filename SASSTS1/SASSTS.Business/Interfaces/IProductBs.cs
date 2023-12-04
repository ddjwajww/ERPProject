using Infrastructure.ApiResponses;
using SASSTS.Model.Dtos.Product;
using SASSTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Business.Interfaces
{
    public interface IProductBs
    {
        Task<ApiResponse<List<ProductGetDto>>> GetAllProducts(string include);
        Task<ApiResponse<List<ProductGetDto>>> GetbyCategoryIdAsync(int categoryId, string include);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
        Task<ApiResponse<NoData>> InsertProduct(ProductPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(ProductPutDto dto);
    }
}
