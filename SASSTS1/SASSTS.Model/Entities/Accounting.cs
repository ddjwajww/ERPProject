using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.Entities
{
    public class Accounting : IEntity
    {
        [Key]public long Id { get; set; }
        public long EmployeeId { get; set; }
        public string Email { get; set; }
        public string UserPassword { get; set; }
        public string? UserName { get; set; }
        public Employee Employee { get; set; }
    }
}
