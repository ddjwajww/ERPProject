using Infrastructure.ApiResponses;
using SASSTS.Model.Dtos.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Business.Interfaces
{
    public interface IStockBs
    {
        Task<ApiResponse<List<StockGetDto>>> GetallStockbyCompanyIdAsync(int companyId, params string[] includeList);
    }
}
