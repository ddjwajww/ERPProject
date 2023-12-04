using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.ProductVM
{
    public class CreateProductVM
    {
        public byte CategoryId { get; set; }
        public byte UnitId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
    }
}
