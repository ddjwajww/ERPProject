using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.Entities
{
    public class Stock : IEntity
    {
        [Key] public int Id { get; set; }
        public int ProductId { get; set; }
        public DateTime CreateTime { get; set; }
        public int Quantity { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public Product Product { get; set; }
        public List<StockOperation> StockOperations { get; set; }
    }
}
