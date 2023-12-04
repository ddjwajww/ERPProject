using Infrastructure.ApiResponses;
using Infrastructure.Models;
using SASSTS.Business.Attributes;
using SASSTS.Business.Controls.Dto;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.TimeReport;
using SASSTS.Model.Entities;

namespace SASSTS.Business.Implementations
{
    public class TimeReportBs : BusinessMap, ITimeReportBs
    {
        private readonly IExceptionValidDto<TimeReport, TimeReportGetDto, TimeReportBs, TimeReportGetDto, TimeReportGetDto> _tr;
        public TimeReportBs(IExceptionValidDto<TimeReport, TimeReportGetDto, TimeReportBs, TimeReportGetDto, TimeReportGetDto> tr) => _tr = tr;
        public async Task<ApiResponse<List<TimeReportGetDto>>> GetAllAsync() => await _tr.GetAllDtosController("TimeReport");
        public async Task<ApiResponse<List<TimeReportGetDto>>> GetallbyCompanyId(int reportId) => await _tr.GetAllDtosController("TimeReport",
            prd => prd.CompanyId == reportId);
        public async Task<ApiResponse<TimeReportGetDto>> GetbyRequestId(int requestId) => await _tr.GetbyFilter("TimeReport", prd => prd.RequestId == requestId);
        public async Task<ApiResponse<NoData>> UpdateRead(int reportId) => await _tr.UpdateAsync("TimeReport", prd => prd.Id == reportId, mdl => mdl.IsRead = true);
    }
}