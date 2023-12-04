using Microsoft.AspNetCore.Mvc;
using SASSTS.Business.Interfaces;
using SASSTS.Model.RequestModels.ProductImagesVM;

namespace SASSTS.WebAPI.Controllers
{
    [ApiController]
    [Route("productImage")]
    public class ProductImageController : BaseController
    {
        private readonly IProductImageService _productImageService;
        public ProductImageController(IProductImageService productImageService) => _productImageService = productImageService;

        [HttpGet("getAllByProduct/{id:int?}")]
        public async Task<IActionResult> GetAllImagesByProduct(int? id) => SendResponse(await _productImageService.GetAllImagesByProduct(new GetAllProductImageByProductVM
        { ProductId = id }));

        [HttpPost("create")]
        public async Task<IActionResult> CreateProductImage([FromForm] CreateProductImageVM createProductImageVM) => SendResponse(
            await _productImageService.CreateProductImage(createProductImageVM));

        [HttpDelete("delete/{id:int?}")]
        public async Task<IActionResult> DeleteProductImage(int? id) => SendResponse(await _productImageService.DeleteProductImage(new DeleteProductImageVM
        { ProductId = id }));
    }
}