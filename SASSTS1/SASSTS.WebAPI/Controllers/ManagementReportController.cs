using Microsoft.AspNetCore.Mvc;
using SASSTS.Business.Interfaces;

namespace SASSTS.WebAPI.Controllers
{
    [Route("managementReport")]
    [ApiController]
    public class ManagementReportController : BaseController
    {
        private readonly IManagementReportBs _managementReportBs;
        public ManagementReportController(IManagementReportBs managementReportBs) => _managementReportBs = managementReportBs;

        [HttpGet("getallbyCompanyId")]
        public async Task<IActionResult> GetallbyCompanyId([FromQuery] int companyId) => SendResponse(await _managementReportBs.GetallbyCompanyId(companyId));

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll() => SendResponse(await _managementReportBs.GetAllAsync());

        [HttpPut("isReadReport")]
        public async Task<IActionResult> IsRead([FromQuery] int reportId) => SendResponse(await _managementReportBs.UpdateRead(reportId));
    }
}