using Microsoft.AspNetCore.Mvc;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.BillAllModel;

namespace SASSTS.WebAPI.Controllers
{
    [Route("bill")]
    [ApiController]
    public class BillController : BaseController
    {
        private readonly IBillBs _billBs;
        public BillController(IBillBs billBs) => _billBs = billBs;

        [HttpPost("createBill")]
        public async Task<IActionResult> CreateBill([FromBody] BillModelItem billModelItem) => SendResponse(await _billBs.CreateAsync(billModelItem));
        
        [HttpGet("getallbyCompanyId")]
        public async Task<IActionResult> GetAllbyCompanyId(int companyId) => SendResponse(await _billBs.GetAllbyCompanyIdAsync(companyId));
    }
}