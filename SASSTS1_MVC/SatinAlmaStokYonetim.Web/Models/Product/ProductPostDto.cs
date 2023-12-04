namespace SatinAlmaStokYonetim.Web.Models.Product
{
    public class ProductPostDto
    {
        public int CategoryId { get; set; }
        public int UnitId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public bool IsDeleted { get; set; }
    }
}
