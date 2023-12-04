using Microsoft.AspNetCore.Mvc;
using SASSTS.Business.Interfaces;

namespace SASSTS.WebAPI.Controllers
{
    [Route("requests")]
    [ApiController]
    public class RequestController : BaseController
    {
        private readonly IRequestBs _requestBs;
        public RequestController(IRequestBs requestBs) => _requestBs = requestBs;

        [HttpGet("getRequests")]//Status = 3 olanlar ve companyNo lar için gerekli
        public async Task<IActionResult> GetRequests([FromQuery] int companyId) => SendResponse(await _requestBs.GetAllRequests(companyId));

        [HttpPost("insertRequest")]
        public async Task<IActionResult> Create([FromBody] dynamic model) => SendResponse(await _requestBs.InsertRequest(model));

        [HttpGet("getbyCompandDep")]
        public async Task<IActionResult> GetbyCompanyId([FromQuery] int companyId, [FromQuery] int departmentId)  //buraya giriş yapan elamanın companyId ve departmentId değeri gelecek.
            => SendResponse(await _requestBs.GetbyCompanyNo(companyId, departmentId));
       
        [HttpGet("getbySA")] //birim amiri tarafından onaylanmış talepleri SA için filtreledim.
        public async Task<IActionResult> GetbySA([FromQuery] int companyId, [FromQuery] int departmentId)  //buraya giriş yapan elamanın companyId ve departmentId değeri gelecek.
            => SendResponse(await _requestBs.GetbySA(companyId, departmentId));

        [HttpGet("getbyrequestId")]
        public async Task<IActionResult> GetbyId([FromQuery] long id) => SendResponse(await _requestBs.GetbyIdAsync(id));
   
        [HttpPut("updateRequest")]//Request onaylama işlemi requestId geliyor bize.
        public async Task<IActionResult> Update([FromQuery] long id) => SendResponse(await _requestBs.UpdateAsync(id));
 
        [HttpDelete("deleteRequest")]//IsActive = 2 ye çekiyor.
        public async Task<IActionResult> DeleteRequest([FromQuery] long id) => SendResponse(await _requestBs.DeleteAsync(id));

        [HttpGet("getallnoRequests")]//Status hariç tüm requestler çekiliyor.
        public async Task<IActionResult> getAllNoStatus([FromQuery] int companyId) => SendResponse(await _requestBs.GetAllRequestsNoStatus(companyId));

        [HttpPut("requeststatusTwo")]
        public async Task<IActionResult> RequestStatusTwo([FromQuery] int requestId) => SendResponse(await _requestBs.RequestStatusTwo(requestId));

        [HttpGet("getallStatusFour")]
        public async Task<IActionResult> GetAllRequestsFour([FromQuery] int companyId, [FromQuery] int departmentId) => SendResponse(
            await _requestBs.GetStatusFour(companyId: companyId, departmentId: departmentId));
    }
}