using Infrastructure.ApiResponses;
using SASSTS.Model.Dtos.Basket;
using SASSTS.Model.Entities;

namespace SASSTS.Business.Interfaces
{
    public interface IBasketBs
    {
        Task<ApiResponse<List<BasketGetDto>>> GetAllBaskets();
        Task<ApiResponse<Basket>> InsertBasket(BasketPostDto dto);
    }
}
