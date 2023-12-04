using Infrastructure.ApiResponses;
using SASSTS.Model.Dtos.Bill;
using SASSTS.Model.Dtos.BillAllModel;

namespace SASSTS.Business.Interfaces
{
    public interface IBillBs
    {
        Task<ApiResponse<NoData>> CreateAsync(BillModelItem billModelItem);
        Task<ApiResponse<List<BillGetDto>>> GetAllbyCompanyIdAsync(int companyId);
    }
}
