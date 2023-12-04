using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.Entities
{
    public class Product : IEntity
    {
        [Key] public int Id { get; set; }
        public int CategoryId { get; set; }
        public int UnitId {get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public bool? IsDeleted { get; set; }
        public Unit Unit { get; set; }
        public List<Stock> Stocks { get; set; }
        public Category Category { get; set; }
        public List<BasketDetail> BasketDetails { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
