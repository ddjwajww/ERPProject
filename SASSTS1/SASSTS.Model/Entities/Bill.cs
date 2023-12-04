using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.Entities
{
    public class Bill : IEntity
    {
        [Key] public int Id { get; set; }
        public long RequestId { get; set; }
        public int SupplierId { get; set; }
        public string BillType { get; set; }
        public DateTime BillDate { get; set; }
        public string BillNumber { get; set; }
        public decimal Discount { get; set; }
        public decimal ProductAmount { get; set; }
        public decimal TotalKDV { get; set; }
        public decimal BillTotalPrice { get; set; }
        public string Currency { get; set; }
        public int CompanyId { get; set; }
        public List<BillDetail> BillDetails{ get; set; }
    }
}
