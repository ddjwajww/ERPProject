using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.Entities
{
    public class BasketDetail : IEntity
    {
        [Key] public long Id { get; set; }
        public int ProductId { get; set; }
        public int BasketId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitQuantity { get; set; }
        public byte DetailStatus { get; set; }

        public Product Product { get; set; }
        public Basket Basket { get; set; }
    }
}
