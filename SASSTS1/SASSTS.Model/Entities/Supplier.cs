using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.Entities
{
    public class Supplier : IEntity
    {
        [Key] public int Id { get; set; }
        public string CompanyMail { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPhone { get; set; }
        public bool IsDeleted { get; set; }
        public List<Offer> Offers { get; set; }
    }
}
