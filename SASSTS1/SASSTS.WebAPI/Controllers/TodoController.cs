using Microsoft.AspNetCore.Mvc;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.Todo;

namespace SASSTS.WebAPI.Controllers
{
    [Route("todo")]
    [ApiController]
    public class TodoController : BaseController
    {
        private readonly ITodosBs _todoBs;
        public TodoController(ITodosBs todoBs) => _todoBs = todoBs;

        [HttpPost("deleteTodo")]
        public async Task<IActionResult> DeleteTodo([FromBody] int[] dizi) => SendResponse(await _todoBs.DeleteAsync(dizi));

        [HttpPut("updateTodo")]
        public async Task<IActionResult> UpdateTodo([FromBody] TodoPutDto updateDto) => SendResponse(await _todoBs.UpdateAsync(updateDto));

        [HttpGet("getallbyEmployeeId")]
        public async Task<IActionResult> GetAllbyEmployeeId([FromQuery] long employeeId) => SendResponse(await _todoBs.GetallbyEmployeeIdAsync(employeeId));

        [HttpGet("getallTodo")]
        public async Task<IActionResult> GetAllTodo() => SendResponse(await _todoBs.GetAllTodosAsync());

        [HttpPost("insertTodo")]
        public async Task<IActionResult> InsertTodo([FromBody] TodoPostDto dto) => SendResponse(await _todoBs.InsertAsync(dto));
    }
}