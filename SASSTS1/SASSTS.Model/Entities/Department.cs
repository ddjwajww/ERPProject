using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.Entities
{
    public class Department : IEntity
    {
        [Key] public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentNo { get; set; }
        public bool? IsDeleted { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
