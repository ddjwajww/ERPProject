using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.Entities
{
    public class Role : IEntity
    {
        [Key] public int Id { get; set; }
        public string RoleName { get; set; }
        public bool? IsDeleted { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
