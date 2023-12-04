using Infrastructure.Models;

namespace SASSTS.Model.Dtos.Basket
{
    public class BasketGetDto : IDto
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
