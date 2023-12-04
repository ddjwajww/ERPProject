using Infrastructure.Models;

namespace SASSTS.Model.Dtos.Employee
{
    public class EmployeeGetUIDto : IDto
    {
        public string RoleName { get; set; }
        public string AuthorityName { get; set; }
        public string CompanyName { get; set; }
        public string DepartmentName { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string? IdentityNo { get; set; }
    }
}
