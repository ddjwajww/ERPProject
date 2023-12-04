using Microsoft.AspNetCore.Mvc;
using SASSTS.Business.Interfaces;

namespace SASSTS.WebAPI.Controllers
{
    [ApiController]
    [Route("processHistories")]
    public class ProcessHistoryController : BaseController
    {
        private readonly IProcessHistoryBs _processHistoryBs;
        public ProcessHistoryController(IProcessHistoryBs processHistoryBs) => _processHistoryBs = processHistoryBs;

        [HttpGet("getallbyemployeeId")]
        public async Task<IActionResult> GetallByEmployeeId([FromQuery] int employeeId) => SendResponse(await _processHistoryBs.GetallbyEmployeeId(employeeId));
    }
}