using Infrastructure.ApiResponses;
using SASSTS.Model.Dtos.Support;
using SASSTS.Model.Entities;

namespace SASSTS.Business.Interfaces
{
    public interface ISupportBs
    {
        Task<ApiResponse<List<Support>>> GetAll();
        Task<ApiResponse<NoData>> InsertSupport(SupportPostDto dto);
    }
}