using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.Entities
{
    public class BillDetail : IEntity
    {
        [Key] public int Id { get; set; }
        public int BillId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int ProductKdv { get; set; }
        public int ProductQuantity { get; set; }
        public Bill Bill { get; set; }
    }
}
