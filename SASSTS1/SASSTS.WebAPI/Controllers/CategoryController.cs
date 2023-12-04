using Microsoft.AspNetCore.Mvc;
using SASSTS.Business;
using SASSTS.Business.Implementations;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.Category;

namespace SASSTS.WebAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly ICategoryBs _categoryBs;
        public CategoryController(ICategoryBs categoryBs) => _categoryBs = categoryBs;

        [HttpGet("getCategories")]
        public async Task<IActionResult> GetCategories() => SendResponse(await _categoryBs.GetAllCategories());

        [HttpGet("getbyId")]
        public async Task<IActionResult> GetbyId([FromQuery] byte id) => SendResponse(await _categoryBs.GetbyIdAsync(id));

        [HttpPost("insertCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryPostDto dto) => SendResponse(await _categoryBs.InsertCategory(dto));

        [HttpDelete("deleteCategory")]
        public async Task<IActionResult> Delete([FromQuery] int id) => SendResponse(await _categoryBs.DeleteAsync(id));

        [HttpPut("updateCategory")]
        public async Task<IActionResult> Update([FromBody] CategoryPutDto dto) => SendResponse(await _categoryBs.UpdateAsync(dto));
    }
}