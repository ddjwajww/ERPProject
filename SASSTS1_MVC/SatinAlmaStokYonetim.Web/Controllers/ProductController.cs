using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.ApiServices;
using SatinAlmaStokYonetim.Web.Models.Product;
using SatinAlmaStokYonetim.Web.Models.Response;
using System.Text.Json;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class ProductController : Controller
    {

        private readonly IHttpApiService _httpApiService;
        public ProductController(IHttpApiService httpApiService) => _httpApiService = httpApiService;

        [HttpPost]//Burada id değeri Int32 ye dönüşür eğer hata alınırsa.
        public async Task<IActionResult> GetCategory([FromBody] string id)
        {
            var Id = Convert.ToInt32(id);
            return await _httpApiService.GetData<List<ProductItem>>($"api/products/getallbycategoryId?categoryId={Id}");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct() =>
            await _httpApiService.GetData<List<ProductItem>>("api/products/getProducts");

        [HttpPost]
        public async Task<IActionResult> InsertProduct([FromBody] ProductPostDto dto) =>
             await _httpApiService.PostData<NoData>("api/products/insertProduct", JsonSerializer.Serialize(dto));

        [HttpPost]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductPutDto dto) =>
            await _httpApiService.PutData<NoData>("api/products/updateProduct", JsonSerializer.Serialize(dto));

        [HttpPost]
        public async Task<IActionResult> DeleteProduct([FromBody] int id) =>
           await _httpApiService.DeleteData<NoData>($"api/products/deleteProduct?productId={id}");
    }
}