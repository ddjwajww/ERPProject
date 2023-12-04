using Microsoft.AspNetCore.Mvc;
using SASSTS.Business.Interfaces;

namespace SASSTS.WebAPI.Controllers
{
    [Route("billdetail")]
    [ApiController]
    public class BillDetailController : BaseController
    {
        private readonly IBillDetailBs _billDetailBs;
        public BillDetailController(IBillDetailBs billDetailBs) => _billDetailBs = billDetailBs;

        [HttpGet("getallbilldetailbybillId")]
        public async Task<IActionResult> GetallBillDetail([FromQuery] int billId) => SendResponse(await _billDetailBs.GetallDetail(billId));
    }
}