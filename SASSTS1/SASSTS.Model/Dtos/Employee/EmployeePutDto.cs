using Infrastructure.Models;

namespace SASSTS.Model.Dtos.Employee
{
    public class EmployeePutDto : IDto
    {
        public long Id { get; set; }
        public int RoleId { get; set; }
        public int AuthorityId { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string IdentityNo { get; set; }

    }
}