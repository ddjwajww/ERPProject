using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.ApiServices;
using SatinAlmaStokYonetim.Web.Models.Response;
using SatinAlmaStokYonetim.Web.Models.Todo;
using System.Text.Json;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class TodoController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        public TodoController(IHttpApiService httpApiService) => _httpApiService = httpApiService;

        [HttpPost]
        public async Task<IActionResult> GetAllTodoByEmployee([FromBody] int id) =>
            await _httpApiService.GetData<List<TodoItem>>($"todo/getallbyEmployeeId?employeeId={id}");

        [HttpPost]
        public async Task<IActionResult> InsertTodo([FromBody] TodoCreateItem item) =>
            await _httpApiService.PostData<NoData>("todo/insertTodo", JsonSerializer.Serialize(item));

        [HttpPost]
        public async Task<IActionResult> UpdateTodo([FromBody] TodoPutDto dto) =>
            await _httpApiService.PutData<NoData>("todo/updateTodo", JsonSerializer.Serialize(dto));

        [HttpPost]
        public async Task<IActionResult> DeleteTodo([FromBody] int[] ids) =>
            await _httpApiService.PostData<NoData>("todo/deleteTodo", JsonSerializer.Serialize(ids));
    }
}