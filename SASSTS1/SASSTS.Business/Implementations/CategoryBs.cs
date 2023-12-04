using Infrastructure.ApiResponses;
using Infrastructure.Models;
using SASSTS.Business.Controls.Dto;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.Category;
using SASSTS.Model.Entities;

namespace SASSTS.Business.Implementations
{
    public class CategoryBs : BusinessMap, ICategoryBs
    {
        private readonly IExceptionValidDto<Category, CategoryGetDto, CategoryBs, CategoryPostDto, CategoryPutDto> _categoryBs;
        public CategoryBs(IExceptionValidDto<Category, CategoryGetDto, CategoryBs, CategoryPostDto, CategoryPutDto> categoryBs) => _categoryBs = categoryBs;
        public async Task<ApiResponse<List<CategoryGetDto>>> GetAllCategories() => await _categoryBs.GetAllDtosController("Category", x => x.IsDeleted == false);
        public async Task<ApiResponse<CategoryGetDto>> GetbyIdAsync(byte id) => await _categoryBs.GetbyFilter("Category", x => x.Id == id && x.IsDeleted == false);
        public async Task<ApiResponse<NoData>> InsertCategory(CategoryPostDto dto) => await _categoryBs.InsertAsyncNoData(dto, "Category", x => x.IsDeleted = false,
            x => x.CategoryName == dto.CategoryName);
        public async Task<ApiResponse<NoData>> DeleteAsync(int id) => await _categoryBs.DeleteAsync("Category", c => c.Id == id, m => m.IsDeleted = true);
        public async Task<ApiResponse<NoData>> UpdateAsync(CategoryPutDto dto) => await _categoryBs.UpdateDto(dto, "Category", null!,
            x => x.CategoryName == dto.CategoryName); 
    }
}