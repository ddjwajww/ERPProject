using Microsoft.AspNetCore.Mvc;
using SASSTS.Business.Interfaces;

namespace SASSTS.WebAPI.Controllers
{
    [Route("timeReport")]
    [ApiController]
    public class TimeReportController : BaseController
    {
        private readonly ITimeReportBs _timeReportBs;
        public TimeReportController(ITimeReportBs timeReportBs) => _timeReportBs = timeReportBs;

        [HttpGet("getalltimeReports")]
        public async Task<IActionResult> GetAllReports() => SendResponse(await _timeReportBs.GetAllAsync());

        [HttpGet("getbyrequestId")]
        public async Task<IActionResult> GetbyRequestId([FromQuery] int requestId) => SendResponse(await _timeReportBs.GetbyRequestId(requestId));

        [HttpGet("getallbyCompanyId")]
        public async Task<IActionResult> GetallByCompanyId([FromQuery] int companyId) => SendResponse(await _timeReportBs.GetallbyCompanyId(companyId));

        [HttpPut("isReadput")]
        public async Task<IActionResult> UpdateTimeReport([FromQuery] int reportId) => SendResponse(await _timeReportBs.UpdateRead(reportId));
    }
}