using Infrastructure.Models;

namespace SASSTS.Model.Dtos.Role
{
    public class RoleGetDto : IDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}
