using Infrastructure.ApiResponses;
using Infrastructure.Models;
using SASSTS.Business.Attributes;
using SASSTS.Business.Controls.Dto;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.BasketDetail;
using SASSTS.Model.Entities;

namespace SASSTS.Business.Implementations
{
    public class BasketDetailBs : BusinessMap, IBasketDetailBs
    {
        private readonly IExceptionValidDto<BasketDetail, BasketDetailGetDto, BasketDetailBs, BasketDetailPostDto, BasketDetailGetDto> _bs;
        public BasketDetailBs(IExceptionValidDto<BasketDetail, BasketDetailGetDto, BasketDetailBs, BasketDetailPostDto, BasketDetailGetDto> bs) => _bs = bs;
        public async Task<ApiResponse<List<BasketDetailGetDto>>> GetAllBasketDetail(params string[] includeList) =>
            await _bs.GetAllDtosController("BasketDetail", includeList: includeList);
        public async Task<ApiResponse<NoData>> Insert(BasketDetailPostDto dto) => await _bs.InsertAsyncNoData(dto, "BasketDetail");

        public async Task<ApiResponse<List<BasketDetailGetDto>>> GetAllRequestId(int id, params string[] includeList) => 
            await _bs.GetAllDtosController("BasketDetail", prd => prd.BasketId == id, includeList: includeList);
    }
}