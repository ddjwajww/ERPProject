using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.ApiServices;
using SatinAlmaStokYonetim.Web.Models.Bill;
using SatinAlmaStokYonetim.Web.Models.Response;
using System.Text.Json;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class BillController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        private readonly IValidator<BillModel> _validatorBillModel;
        public BillController(IHttpApiService httpApiService, IValidator<BillModel> validatorBillModel)
        {
            _httpApiService = httpApiService;
            _validatorBillModel = validatorBillModel;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBill([FromBody] BillModel model) =>
            await _httpApiService.PostData<NoData>("bill/createBill", JsonSerializer.Serialize(model));

        [HttpPost]
        public async Task<IActionResult> GetallBillbyCompanyId([FromBody] int companyId) =>
            await _httpApiService.GetData<List<BillGetDto>>($"bill/getallbyCompanyId?companyId={companyId}");

        [HttpPost]
        public async Task<IActionResult> GetBillDetailbyBillId([FromBody] int billId) =>
            await _httpApiService.GetData<List<BillDetailItem>>($"billdetail/getallbilldetailbybillId?billId={billId}");
    }
}