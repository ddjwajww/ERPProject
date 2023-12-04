using Microsoft.AspNetCore.Mvc;
using SASSTS.Business.Interfaces;
using SASSTS.Model.RequestModels.EmployeeImagesVM;

namespace SASSTS.WebAPI.Controllers
{
    [ApiController]
    [Route("employeeImage")]
    public class EmployeetImageController : BaseController
    {
        private readonly IEmployeeImageService _employeeImageService;
        public EmployeetImageController(IEmployeeImageService employeeImageService) => _employeeImageService = employeeImageService;

        [HttpGet("getAllByEmployeet/{id:int?}")]
        public async Task<IActionResult> GetAllImagesByEmployee(int? id) => SendResponse(await _employeeImageService.GetAllImagesByEmployee(new GetAllEmployeeImageByEmployeeVM
        { EmployeeId = id }));

        [HttpPost("create")]
        public async Task<IActionResult> CreateEmployeetImage([FromForm] CreateEmployeeImageVM createEmployeeImageVM) => SendResponse(
            await _employeeImageService.CreateEmployeeImage(createEmployeeImageVM));

        [HttpDelete("delete/{id:int?}")]
        public async Task<IActionResult> DeleteEmployeeImage(int? id) => SendResponse(await _employeeImageService.DeleteEmployeeImage(new DeleteEmployeeImageVM
        { EmployeeId = id }));
    }
}