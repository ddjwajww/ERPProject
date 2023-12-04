using SatinAlmaStokYonetim.Web.Code;

namespace SatinAlmaStokYonetim.Web.Models.Offer
{
    public class OfferPostDto
    {
        public int CompanyId { get; set; }
        public long RequestId { get; set; }
        public int SupplierId { get; set; }
        public string? OfferDescription { get; set; }
        public string PriceCurrency { get; set; }
        public decimal OfferPrice { get; set; }
    }
}
