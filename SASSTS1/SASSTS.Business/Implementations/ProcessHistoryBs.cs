using Infrastructure.ApiResponses;
using Infrastructure.Models;
using SASSTS.Business.Attributes;
using SASSTS.Business.Controls.Dto;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.ProcessHistory;
using SASSTS.Model.Entities;

namespace SASSTS.Business.Implementations
{
    public class ProcessHistoryBs : BusinessMap, IProcessHistoryBs
    {
        private readonly IExceptionValidDto<ProcessHistory, ProcessHistoryGetDto, ProcessHistoryBs, ProcessHistoryPostDto, ProcessHistoryGetDto> _ph;
        public ProcessHistoryBs(IExceptionValidDto<ProcessHistory, ProcessHistoryGetDto, ProcessHistoryBs, ProcessHistoryPostDto, ProcessHistoryGetDto> ph) => _ph = ph;
        public async Task<ApiResponse<List<ProcessHistoryGetDto>>> GetallbyEmployeeId(int employeeId) => await _ph.GetAllDtosController("ProcessHistory", prd => prd.EmployeeId == employeeId);
    }
}