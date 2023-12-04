using Infrastructure.ApiResponses;
using SASSTS.Model.Dtos.Accounting;
using SASSTS.Model.Dtos.Employee;
using SASSTS.Model.RequestModels.AccountingVM;

namespace SASSTS.Business.Interfaces
{
    public interface IEmployeeBs
    {
        Task<ApiResponse<List<EmployeeGetDto>>> GetAllAsync();
        Task<ApiResponse<NoData>> Update222(EAMPUTDTO dto);
        Task<ApiResponse<UpdateUserVM>> GetbyId(int employeeId, string include);
        Task<ApiResponse<NoData>> Delete(int employeeId);
        Task<ApiResponse<NoData>> CreateEmployee(EmployeePostDto dto);
        Task<ApiResponse<List<EmployeeGetDto>>> GetallbyCompanyId(int companyId, params string[] includeList);
        Task<ApiResponse<NoData>> UpdateAsync(EmployeePutDto dto);
        Task<ApiResponse<List<EmployeeGetUIDto>>> GetAllEmployeebyJoin(params string[] includeList);
    }
}
