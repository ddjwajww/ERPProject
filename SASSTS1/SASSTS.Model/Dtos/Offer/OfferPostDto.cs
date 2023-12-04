using Infrastructure.Models;

namespace SASSTS.Model.Dtos.Offer
{
    public class OfferPostDto : IDto
    {
        public int CompanyId { get; set; }
        public long RequestId { get; set; }
        public int SupplierId { get; set; }
        public string? OfferDescription { get; set; }
        public decimal OfferPrice { get; set; }
        public string PriceCurrency { get; set; }
    }
}
