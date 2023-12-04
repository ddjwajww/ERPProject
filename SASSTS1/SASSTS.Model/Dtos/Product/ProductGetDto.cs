using Infrastructure.Models;

namespace SASSTS.Model.Dtos.Product
{
    public class ProductGetDto : IDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
    }
}
