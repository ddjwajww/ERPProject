using Infrastructure.ApiResponses;
using Infrastructure.Models;
using SASSTS.Business.Attributes;
using SASSTS.Business.Controls.Dto;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.Stock;
using SASSTS.Model.Entities;

namespace SASSTS.Business.Implementations
{
    public class StockBs : BusinessMap, IStockBs
    {
        private readonly IExceptionValidDto<Stock, StockGetDto, StockBs, StockPostDto, StockGetDto> _stock;
        public StockBs(IExceptionValidDto<Stock, StockGetDto, StockBs, StockPostDto, StockGetDto> stock) => _stock = stock;
        public async Task<ApiResponse<List<StockGetDto>>> GetallStockbyCompanyIdAsync(int companyId, params string[] includeList) =>
            await _stock.GetAllDtosController("Stock", prd => prd.CompanyId == companyId, includeList: includeList);
    }
}