namespace SatinAlmaStokYonetim.Web.Models.Bill
{
    public class BillDetailItem
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int ProductKdv { get; set; }
        public int ProductQuantity { get; set; }
    }
}
