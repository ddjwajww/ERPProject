using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.Entities
{
    public class Request : IEntity
    {
        [Key] public long Id { get; set; }
        public long EmployeeId { get; set; }
        public int BasketId { get; set; }
        public string RequestNo { get; set; }
        public byte RequestStatus { get; set; }

        public List<Offer> Offers { get; set; }
        public Basket Basket { get; set; }
        public Employee Employee { get; set; }
    }
}
