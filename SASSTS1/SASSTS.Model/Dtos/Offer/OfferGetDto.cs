using Infrastructure.Models;

namespace SASSTS.Model.Dtos.Offer
{
    public class OfferGetDto : IDto
    {
        public long Id { get; set; }
        public long RequestId { get; set; }
        public string CompanyName { get; set; }//Supplierdan gelecek.
        public DateTime OfferTime { get; set; }
        public byte OfferStatus { get; set; }
        public string OfferNo { get; set; }
        public string? OfferDescription { get; set; }
        public decimal OfferPrice { get; set; }
        public string PriceCurrency { get; set; }
    }
}
