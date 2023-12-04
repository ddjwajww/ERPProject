using Infrastructure.ApiResponses;
using Infrastructure.Models;
using SASSTS.Business.Attributes;
using SASSTS.Business.Controls.Dto;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.Product;
using SASSTS.Model.Entities;

namespace SASSTS.Business.Implementations
{
    public class ProductBs : BusinessMap, IProductBs
    {
        private readonly IExceptionValidDto<Product, ProductGetDto, ProductBs, ProductPostDto, ProductPutDto> _prd;
        public ProductBs(IExceptionValidDto<Product, ProductGetDto, ProductBs, ProductPostDto, ProductPutDto> prd) => _prd = prd;
        public async Task<ApiResponse<List<ProductGetDto>>> GetAllProducts(string include) => await _prd.GetAllDtosController("Product", predicate: x => x.IsDeleted == false, includeList: include);

        public async Task<ApiResponse<List<ProductGetDto>>> GetbyCategoryIdAsync(int categoryId, string include) => await _prd.GetAllDtosController("Product", ctg => ctg.CategoryId == categoryId && ctg.IsDeleted == false, includeList: include);
        public async Task<ApiResponse<NoData>> InsertProduct(ProductPostDto dto) => await _prd.InsertAsyncNoData(dto, "Product"); 
        public async Task<ApiResponse<NoData>> DeleteAsync(int id) => await _prd.DeleteAsync("Product", prd => prd.Id == id, mdl => mdl.IsDeleted = true);
        public async Task<ApiResponse<NoData>> UpdateAsync(ProductPutDto dto) => await _prd.UpdateDto(dto, "Product", null!);
    }
}