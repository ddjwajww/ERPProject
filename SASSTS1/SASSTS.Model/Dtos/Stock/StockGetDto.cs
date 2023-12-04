using Infrastructure.Models;

namespace SASSTS.Model.Dtos.Stock
{
    public class StockGetDto : IDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string ProductName { get; set; }
        public int CompanyId { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
    }
}