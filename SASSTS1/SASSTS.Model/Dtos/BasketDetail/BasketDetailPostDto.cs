using Infrastructure.Models;

namespace SASSTS.Model.Dtos.BasketDetail
{
    public class BasketDetailPostDto : IDto
    {
        public int ProductId { get; set; }
        public long BasketId { get; set; }
        public int Quantity { get; set; }
        public float UnitQuantity { get; set; }
        public byte DetailStatus { get; set; }
    }
}
