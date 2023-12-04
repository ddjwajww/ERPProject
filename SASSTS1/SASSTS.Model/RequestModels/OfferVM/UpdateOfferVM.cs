using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.OfferVM
{
    public class UpdateOfferVM
    {
        [Key] public int Id { get; set; }
        public long RequestId { get; set; }
        public int SupplierId { get; set; }
        public DateTime OfferTime { get; set; }
        public byte OfferStatus { get; set; }
        public string OfferNo { get; set; }
        public string? OfferDescription { get; set; }
        public decimal OfferPrice { get; set; }
        public string PriceCurrency { get; set; }
    }
}
