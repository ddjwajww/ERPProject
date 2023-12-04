using Infrastructure.ApiResponses;
using SASSTS.Model.Dtos.BasketDetail;
using SASSTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Business.Interfaces
{
    public interface IBasketDetailBs
    {
        Task<ApiResponse<List<BasketDetailGetDto>>> GetAllBasketDetail(params string[] includeList);
        Task<ApiResponse<List<BasketDetailGetDto>>> GetAllRequestId(int id, params string[] includeList);
        Task<ApiResponse<NoData>> Insert(BasketDetailPostDto dto);
    }
}
