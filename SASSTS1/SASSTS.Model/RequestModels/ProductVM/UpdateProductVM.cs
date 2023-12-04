using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.ProductVM
{
    public class UpdateProductVM
    {
        [Key] public int Id { get; set; }
        public byte CategoryId { get; set; }
        public byte UnitId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
    }
}
