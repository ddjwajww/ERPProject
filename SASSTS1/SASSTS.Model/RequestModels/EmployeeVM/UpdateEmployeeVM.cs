using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.EmployeeVM
{
    public class UpdateEmployeeVM
    {
        [Key] public long Id { get; set; }
        public byte RoleId { get; set; }
        public byte AuthorityId { get; set; }
        public byte CompanyId { get; set; }
        public byte DepartmentId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }


    }
}
