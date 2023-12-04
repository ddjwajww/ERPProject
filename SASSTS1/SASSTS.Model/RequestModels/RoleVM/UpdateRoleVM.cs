using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.RoleVM
{
    public class UpdateRoleVM
    {
        [Key] public byte Id { get; set; }
        public string RoleName { get; set; }
    }
}
