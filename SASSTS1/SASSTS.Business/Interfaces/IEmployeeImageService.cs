using Infrastructure.ApiResponses;
using SASSTS.Model.Dtos.Employee;
using SASSTS.Model.RequestModels.EmployeeImagesVM;

namespace SASSTS.Business.Interfaces
{
    public interface IEmployeeImageService
    {
        Task<ApiResponse<List<EmployeeImageDto>>> GetAllImagesByEmployee(GetAllEmployeeImageByEmployeeVM getByEmployeeVM);
        Task<ApiResponse<int>> CreateEmployeeImage(CreateEmployeeImageVM createEmployeeImageVM);
        Task<ApiResponse<int>> DeleteEmployeeImage(DeleteEmployeeImageVM deleteEmployeeImageVM);
    }
}
