using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.RoleVM
{
    public class GetRoleByIdVM
    {
        [Key] public byte Id { get; set; }
    }
}
