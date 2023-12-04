using Infrastructure.ApiResponses;
using SASSTS.Model.Dtos.StockOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Business.Interfaces
{
    public interface IStockOperationBs
    {
        Task<ApiResponse<List<StockOperationGetDto>>> GetallOperation(int stockId, string include);
        Task<ApiResponse<NoData>> LogOutStock(StockOperationPostDto dto);
    }
}
