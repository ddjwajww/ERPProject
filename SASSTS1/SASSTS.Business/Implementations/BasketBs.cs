using Infrastructure.ApiResponses;
using Infrastructure.Models;
using SASSTS.Business.Controls.Dto;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.Basket;
using SASSTS.Model.Entities;

namespace SASSTS.Business.Implementations
{
    public class BasketBs : BusinessMap, IBasketBs
    {
        private readonly IExceptionValidDto<Basket, BasketGetDto, BasketBs, BasketPostDto, BasketGetDto> _basketBs;
        public BasketBs(IExceptionValidDto<Basket, BasketGetDto, BasketBs, BasketPostDto, BasketGetDto> basketBs) {_basketBs = basketBs; }
        public async Task<ApiResponse<List<BasketGetDto>>> GetAllBaskets() => await _basketBs.GetAllDtosController("Basket");
        public async Task<ApiResponse<Basket>> InsertBasket(BasketPostDto dto) => await _basketBs.InsertAsync(dto, "Basket");
    }
}