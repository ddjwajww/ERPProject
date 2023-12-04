using Infrastructure.ApiResponses;
using SASSTS.Model.Dtos.ProcessHistory;

namespace SASSTS.Business.Interfaces
{
    public interface IProcessHistoryBs
    {
        Task<ApiResponse<List<ProcessHistoryGetDto>>> GetallbyEmployeeId(int employeeId);
    }
}
