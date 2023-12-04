using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.Entities
{
    public class Authority : IEntity
    {
        [Key] public int Id { get; set; }
        public string AuthorityName { get; set; }
        public bool? IsDeleted { get; set; }
        public List<Employee> Employees { get; set; }
    }
}