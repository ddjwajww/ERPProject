using Infrastructure.Models;

namespace SASSTS.Model.Dtos.Role
{
    public class RolePutDto : IDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
