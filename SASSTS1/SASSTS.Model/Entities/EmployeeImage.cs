using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.Entities
{
    public class EmployeeImage : IEntity
    {
        [Key] public int Id { get; set; }
        public long EmployeeId { get; set; }
        public string Path { get; set; }
        public Employee Employee { get; set; }
    }
}
