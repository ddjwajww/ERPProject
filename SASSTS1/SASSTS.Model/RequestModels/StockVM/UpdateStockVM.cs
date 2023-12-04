using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.StockVM
{
    public class UpdateStockVM
    {
        [Key] public int Id { get; set; }
        public int ProductId { get; set; }
        public DateTime CreateTime { get; set; }
        public int Quantity { get; set; }
        public byte CompanyId { get; set; }
    }
}
