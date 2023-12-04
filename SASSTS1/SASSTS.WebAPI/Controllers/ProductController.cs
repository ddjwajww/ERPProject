using Microsoft.AspNetCore.Mvc;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.Product;
using System.Runtime.CompilerServices;

namespace SASSTS.WebAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductBs _productBs;
        public ProductController(IProductBs productBs) => _productBs = productBs;

        [HttpGet("getProducts")]
        public async Task<IActionResult> GetProducts() => SendResponse(await _productBs.GetAllProducts("Unit"));

        [HttpGet("getallbycategoryId")]
        public async Task<IActionResult> GetbyCategoryId([FromQuery] byte categoryId) => SendResponse(await _productBs.GetbyCategoryIdAsync(categoryId, "Unit"));

        [HttpPost("insertProduct")]
        public async Task<IActionResult> InsertProduct([FromBody] ProductPostDto dto) => SendResponse(await _productBs.InsertProduct(dto));

        [HttpDelete("deleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromQuery] int productId) => SendResponse(await _productBs.DeleteAsync(productId));

        [HttpPut("updateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductPutDto dto) => SendResponse(await _productBs.UpdateAsync(dto));
    }
}