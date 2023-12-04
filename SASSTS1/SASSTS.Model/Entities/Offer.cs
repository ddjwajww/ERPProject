using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.Entities
{
    public class Offer : IEntity
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
        public Supplier Supplier { get; set; }
        public Request Request { get; set; }
    }
}
