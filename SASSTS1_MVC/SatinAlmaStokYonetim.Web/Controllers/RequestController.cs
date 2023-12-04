using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.ApiServices;
using SatinAlmaStokYonetim.Web.Models.IdValue;
using SatinAlmaStokYonetim.Web.Models.Request;
using SatinAlmaStokYonetim.Web.Models.Response;
using System.Text.Json;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class RequestController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        public RequestController(IHttpApiService httpApiService) => _httpApiService = httpApiService;

        [HttpPost]
        public async Task<IActionResult> GetbyCompanyIdAndDepartmanId([FromBody] CompDepId id) =>
            await _httpApiService.GetData<List<RequestItem>>($"requests/getbyCompandDep?companyId={id.companyId}&departmentId={id.departmentId}");

        [HttpPost]
        public async Task<IActionResult> GetbyCompanyId([FromBody] int companyId) =>
            await _httpApiService.GetData<List<RequestItem>>($"requests/getbyCompany?companyId={companyId}");

        [HttpPost]
        public async Task<IActionResult> GetbySA([FromBody] CompDepId id) =>
            await _httpApiService.GetData<List<RequestItem>>($"requests/getbySA?companyId={id.companyId}&departmentId={id.departmentId}");

        [HttpPost]
        public async Task<IActionResult> SuccessRequest([FromBody] int requestId) =>
            await _httpApiService.PutData<NoData>($"requests/updateRequest?id={requestId}", JsonSerializer.Serialize(requestId));

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] int id) =>
            await _httpApiService.DeleteData<NoData>($"requests/deleteRequest?id={id}");

        [HttpPost]
        public async Task<IActionResult> StatusTwo([FromBody] int requestId) =>
            await _httpApiService.PutData<NoData>($"requests/requeststatusTwo?requestId={requestId}", JsonSerializer.Serialize(requestId));

        [HttpPost] //RequestStatus = 3 olanlar ve companyId ye göre o değeri  sağlayan talepler için requestler için get etme eylemi
        public async Task<IActionResult> GetStatusThree([FromBody] int companyId) =>
            await _httpApiService.GetData<List<RequestItem>>($"requests/getRequests?companyId={companyId}");

        [HttpPost]
        public async Task<IActionResult> GetStatusFor([FromBody] CompDepId item) =>
            await _httpApiService.GetData<List<RequestItem>>($"requests/getallStatusFour?companyId={item.companyId}&departmentId={item.departmentId}");

        [HttpPost]//Tüm requestler çekiliyor status hariç
        public async Task<IActionResult> GetAllNoStatus([FromBody] int companyId) =>
            await _httpApiService.GetData<List<RequestItem>>($"requests/getallnoRequests?companyId={companyId}");
    }
}