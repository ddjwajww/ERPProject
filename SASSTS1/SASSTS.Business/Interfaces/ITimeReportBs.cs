using Infrastructure.ApiResponses;
using SASSTS.Model.Dtos.TimeReport;
using SASSTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Business.Interfaces
{
    public interface ITimeReportBs
    {
        Task<ApiResponse<List<TimeReportGetDto>>> GetallbyCompanyId(int reportId);
        Task<ApiResponse<TimeReportGetDto>> GetbyRequestId(int requestId);
        Task<ApiResponse<List<TimeReportGetDto>>> GetAllAsync();
        Task<ApiResponse<NoData>> UpdateRead(int reportId);
    }
}
