using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.Entities
{
    public class Company : IEntity
    {
        [Key] public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNo { get; set; }
        public bool? IsDeleted { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Stock> Stocks { get; set; }
    }
}
