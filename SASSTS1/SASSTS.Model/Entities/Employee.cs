using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.Entities
{
    public class Employee : IEntity
    {
        [Key] public long Id { get; set; }
        public int RoleId { get; set; }
        public int AuthorityId { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; } = true;
        public string? IdentityNo { get; set; }
        public List<Accounting> Accountings { get; set; }
        public Company Company { get; set; }
        public List<ProcessHistory> ProcessHistories { get; set; }
        public Authority Authority { get; set; }
        public Role Role { get; set; }
        public Department Department { get; set; }
        public List<Request> Requests { get; set; }
        public List<Message> Messages { get; set; }
        public List<EmployeeImage> EmployeeImages { get; set; }
        public List<Todo> Todos { get; set; }

        //10 ilişik.
    }
}
