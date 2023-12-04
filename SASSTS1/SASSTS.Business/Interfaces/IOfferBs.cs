using Infrastructure.ApiResponses;
using SASSTS.Model.Dtos.Offer;
using SASSTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Business.Interfaces
{
    public interface IOfferBs
    {
        Task<ApiResponse<List<OfferGetDto>>> GetAllAsync();
        Task<ApiResponse<List<OfferGetDto>>> GetAllbyCompanyIdAsync(int companyId, int requestId, params string[] includeList);
        Task<ApiResponse<NoData>> InsertAsync(Offer offer);
        string RandomUret(int companyId);
        Task<ApiResponse<NoData>> SuccessOffer(int offerId,int requestId);  //Teklif onay verme eylemi
        Task<ApiResponse<NoData>> DeleteOffer(int offerId);
    }
}
