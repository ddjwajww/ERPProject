using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.ApiServices;
using SatinAlmaStokYonetim.Web.Models.Category;
using SatinAlmaStokYonetim.Web.Models.Response;
using System.Text.Json;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        private readonly IValidator<CategoryPostDto> _validatorCategoryPostDto;
        public CategoryController(IHttpApiService httpApiService, IValidator<CategoryPostDto> validatorCategoryPostDto)
        {
            _httpApiService = httpApiService;
            _validatorCategoryPostDto   = validatorCategoryPostDto;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory() =>
            await _httpApiService.GetData<List<CategoryItem>>("api/categories/getCategories");

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryPostDto dto) =>
            await _httpApiService.PostData2<NoData, CategoryPostDto>("api/categories/insertCategory", JsonSerializer.Serialize(dto), dto, _validatorCategoryPostDto);
    }
}