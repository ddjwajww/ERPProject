using Infrastructure.ApiResponses;
using Infrastructure.Models;
using SASSTS.Business.Controls.Dto;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.Unit;
using SASSTS.Model.Entities;

namespace SASSTS.Business.Implementations
{
    public class UnitBs : BusinessMap, IUnitBs
    {
        private readonly IExceptionValidDto<Unit, UnitGetDto, UnitBs, UnitPostDto, UnitPutDto> _unitBs;
        public UnitBs(IExceptionValidDto<Unit, UnitGetDto, UnitBs, UnitPostDto, UnitPutDto> unitBs) => _unitBs = unitBs;
        public async Task<ApiResponse<List<UnitGetDto>>> GetAllUnits() => await _unitBs.GetAllDtosController("Unit", x => x.IsDeleted == false);
        public async Task<ApiResponse<NoData>> InsertUnit(UnitPostDto dto) => await _unitBs.InsertAsyncNoData(dto, "Unit", x => x.IsDeleted = false,
            x => x.UnitName == dto.UnitName);
        public async Task<ApiResponse<NoData>> DeleteAsync(int id) => await _unitBs.DeleteAsync("Unit", u => u.Id == id, m => m.IsDeleted = true);
        public async Task<ApiResponse<NoData>> UpdateAsync(UnitPutDto dto) => await _unitBs.UpdateDto(dto, "Unit", null!, x => x.UnitName == dto.UnitName);
    }
}