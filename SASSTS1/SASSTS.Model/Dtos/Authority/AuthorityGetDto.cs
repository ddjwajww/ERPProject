using Infrastructure.Models;

namespace SASSTS.Model.Dtos.Authority
{
    public class AuthorityGetDto : IDto
    {
        public int Id { get; set; }
        public string AuthorityName { get; set; }
    }
}
