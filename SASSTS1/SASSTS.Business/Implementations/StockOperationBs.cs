using AutoMapper;
using Infrastructure.ApiResponses;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SASSTS.Business.Controls.Dto;
using SASSTS.Business.Interfaces;
using SASSTS.DataAccess.UnitOfWork;
using SASSTS.Model.Dtos.StockOperation;
using SASSTS.Model.Entities;

namespace SASSTS.Business.Implementations
{
    public class StockOperationBs : BusinessMap, IStockOperationBs
    {
        private readonly IMapper _mapper;
        private readonly IUnitWork _unitWork;
        private readonly ILogger<StockOperationBs> _logger;
        public StockOperationBs(IMapper mapper, IUnitWork unitWork, ILogger<StockOperationBs> logger) { _unitWork = unitWork; _mapper = mapper; _logger = logger; }
        public async Task<ApiResponse<List<StockOperationGetDto>>> GetallOperation(int stockId, string include) =>
            await ExceptionValidDto<StockOperation, StockOperationGetDto, StockOperationBs>.GetAllDtosController(_unitWork, "StockOperation",
                _mapper, predicate: x => x.StockId == stockId, include:  include, logger: _logger);
        public async Task<ApiResponse<NoData>> LogOutStock(StockOperationPostDto dto)
        {
            await _unitWork.GetRepository<StockOperation>().InsertAsync(new Model.Entities.StockOperation
            {
                OperationTime = DateTime.Now,
                Quantity = dto.Quantity,
                EmployeeId = dto.EmployeeId,
                StockId = dto.StockId,
                Status = false
            });
            var stock = await _unitWork.GetRepository<Stock>().GetAsync(x => x.Id == dto.StockId);
            stock.Quantity = (stock.Quantity - dto.Quantity);
            await _unitWork.GetRepository<Stock>().UpdateAsync(stock);

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}