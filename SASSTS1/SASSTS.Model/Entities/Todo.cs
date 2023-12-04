using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.Entities
{
    public class Todo : IEntity
    {
        [Key] public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreateTime { get; set; }
        public bool Active { get; set; }
        public long EmployeeId { get; set; }
        public bool IsDeleted { get; set; }
        public Employee Employee { get; set; }
    }
}
