using Microsoft.AspNetCore.Mvc;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.Message;

namespace SASSTS.WebAPI.Controllers
{
    [Route("message")]
    [ApiController]
    public class MessageController : BaseController
    {
        private readonly IMessageBs messageBs;
        public MessageController(IMessageBs ms) => messageBs = ms;

        [HttpPost("insertMessage")]
        public async Task<IActionResult> Create([FromBody] MessagePostDto dto) => SendResponse(await messageBs.InsertAllMessage(dto));
        
        [HttpGet("getallMessage")]
        public async Task<IActionResult> GetAll() => SendResponse(await messageBs.GetAllMessage());

        [HttpGet("getAllbyAuthorityId")]
        public async Task<IActionResult> GetAllbyAuthorityId([FromQuery] int authorityId) => SendResponse(await messageBs.GetAllMessageEmployeebyAuthorityId(authorityId));
    }
}
