namespace SASSTS.Model.RequestModels.AccountingVM
{
    public class RegisterVM
    {
        public string IdentityNumber { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string Image { get; set; }
        public int RoleId { get; set; }
        public int AuthorityId { get; set; }
        public bool IsActive { get; set; }
    }
}
