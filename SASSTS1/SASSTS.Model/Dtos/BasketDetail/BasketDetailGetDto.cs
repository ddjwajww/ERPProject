using Infrastructure.Models;

namespace SASSTS.Model.Dtos.BasketDetail
{
    public class BasketDetailGetDto : IDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime CreatedTime { get; set; }
        public int Quantity { get; set; }
        public decimal UnitQuantity { get; set; }
        public byte DetailStatus { get; set; }
    }
}
