using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.ApiServices;
using SatinAlmaStokYonetim.Web.Models;
using SatinAlmaStokYonetim.Web.Models.Account;
using SatinAlmaStokYonetim.Web.Models.Response;
using SatinAlmaStokYonetim.Web.Models.StockOperation;
using SatinAlmaStokYonetim.Web.Models.Supplier;
using SatinAlmaStokYonetim.Web.Validators.Suppliers;
using System.Text.Json;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class SupplierController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        private readonly IValidator<SupplierItem> _validatorSupplier;
        public SupplierController(IHttpApiService httpApiService, IValidator<SupplierItem> validatorSupplier)
        {
            _httpApiService = httpApiService;
            _validatorSupplier = validatorSupplier;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSupplier() =>
            await _httpApiService.GetData<List<SupplierItem>>("suppliers/getallSupplier");

        [HttpPost]
        public async Task<IActionResult> InsertSupplier([FromBody] SupplierItem item) =>
           await _httpApiService.PostData2<NoData, SupplierItem>("suppliers/insertSupplier", JsonSerializer.Serialize(item),item, _validatorSupplier);
        
    }
}