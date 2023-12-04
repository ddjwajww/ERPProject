using Infrastructure.ApiResponses;
using Microsoft.AspNetCore.Mvc;

namespace SASSTS.WebAPI.Controllers
{
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult SendResponse<T>(ApiResponse<T> response) => new ObjectResult(response) { StatusCode = response.StatusCode };
    }
}
