using SatinAlmaStokYonetim.Web.Models.Account;

namespace SatinAlmaStokYonetim.Web.Models.Employee
{
    public class EmployeePostDto
    {
        public int RoleId { get; set; }
        public int AuthorityId { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string? IdentityNo { get; set; }
        public AccountItem AccountItems { get; set; }
        public string SubjectMessage { get; set; }
        public string Hitap { get; set; }
        public string RegisterMessage { get; set; }
        public string UserNameMessage { get; set; }
        public string PasswordMessage { get; set; }
        public string PasswordResetMessage { get; set; }
        public string? UserPassword { get; set; }
        public string? UserName { get; set; }
    }
}
