using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.ApiServices;
using SatinAlmaStokYonetim.Web.Models.Report;
using SatinAlmaStokYonetim.Web.Models.Response;
using System.Text.Json;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class TimeReportController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        public TimeReportController(IHttpApiService httpApiService) => _httpApiService = httpApiService;

        [HttpPost]
        public async Task<IActionResult> GetallTimeReportbyCompanyId([FromBody] int companyId) =>
            await _httpApiService.GetData<List<TimeReportGetDto>>($"timeReport/getallbyCompanyId?companyId={companyId}");

        [HttpPost]
        public async Task<IActionResult> GetTimeReportbyRequestId([FromBody] int requestId) =>
            await _httpApiService.GetData<TimeReportGetDto>($"timeReport/getbyrequestId?requestId={requestId}");

        [HttpPost]//IsRead olayı okundu olarak işaraetle kısmı
        public async Task<IActionResult> ReadisTimeReport([FromBody] int reportId) =>
            await _httpApiService.PutData<NoData>($"timeReport/isReadput?reportId={reportId}", JsonSerializer.Serialize(reportId));
    }
}