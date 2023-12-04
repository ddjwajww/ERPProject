using Infrastructure.ApiResponses;
using SASSTS.Model.Dtos.Unit;

namespace SASSTS.Business.Interfaces
{
    public interface IUnitBs
    {
        Task<ApiResponse<List<UnitGetDto>>> GetAllUnits();
        Task<ApiResponse<NoData>> InsertUnit(UnitPostDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
        Task<ApiResponse<NoData>> UpdateAsync(UnitPutDto dto);
    }
}
