using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.ApiServices;
using SatinAlmaStokYonetim.Web.Models.Report;
using SatinAlmaStokYonetim.Web.Models.Response;
using System.Text.Json;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class ManagementReportController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        public ManagementReportController(IHttpApiService httpApiService) => _httpApiService = httpApiService;

        [HttpPost]
        public async Task<IActionResult> GetallManagementReportbyCompanyId([FromBody] int companyId) =>
            await _httpApiService.GetData<ManagementReport>($"managementReport/getallbyCompanyId?companyId={companyId}");

        
        [HttpPost]//IsRead olayı okundu olarak işararetle kısmı
        public async Task<IActionResult> ReadisTimeReport([FromBody] int reportId) =>
            await _httpApiService.PutData<NoData>($"managementReport/isReadReport?reportId={reportId}", JsonSerializer.Serialize(reportId));
    }
}