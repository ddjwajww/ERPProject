using Infrastructure.ApiResponses;
using SASSTS.Model.Dtos.Authority;

namespace SASSTS.Business.Interfaces
{
    public interface IAuthorityBs
    {
        Task<ApiResponse<List<AuthorityGetDto>>> GetAllAsync();
        Task<ApiResponse<NoData>> InsertAuthority(AuthorityPostDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
        Task<ApiResponse<NoData>> UpdateAsync(AuthorityPutDto dto);
    }
}
