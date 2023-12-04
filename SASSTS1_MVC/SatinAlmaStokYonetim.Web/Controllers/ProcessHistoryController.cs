using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.ApiServices;
using SatinAlmaStokYonetim.Web.Models.History;
using SatinAlmaStokYonetim.Web.Models.Product;
using SatinAlmaStokYonetim.Web.Models.Response;
using SatinAlmaStokYonetim.Web.Models.Supplier;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class ProcessHistoryController
    {
        private readonly IHttpApiService _httpApiService;
        public ProcessHistoryController(IHttpApiService httpApiService) => _httpApiService = httpApiService;

        [HttpPost]//Burada id değeri Int32 ye dönüşür eğer hata alınırsa.
        public async Task<IActionResult> GetHistories([FromBody] int id) =>
           await _httpApiService.GetData<List<HistoryItem>>($"processHistories/getallbyemployeeId?employeeId={id}");

    }
}
