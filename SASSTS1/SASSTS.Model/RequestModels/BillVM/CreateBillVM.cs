using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.BillVM
{
    public class CreateBillVM
    {
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
    }
}
