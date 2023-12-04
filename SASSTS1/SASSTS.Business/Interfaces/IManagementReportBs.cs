using Infrastructure.ApiResponses;
using SASSTS.Model.Dtos.ManagementReport;

namespace SASSTS.Business.Interfaces
{
    public interface IManagementReportBs
    {
        Task<ApiResponse<List<ManagementReportGetDto>>> GetAllAsync();
        Task<ApiResponse<MRGetDto>> GetallbyCompanyId(int companyId);
        Task<ApiResponse<NoData>> UpdateRead(int reportId);
    }
}
