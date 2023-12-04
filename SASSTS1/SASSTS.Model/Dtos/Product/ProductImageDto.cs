using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.Dtos.Product
{
    public class ProductImageDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Path { get; set; }
    }
}
