using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.AuthorityVM
{
    public class UpdateAuthorityVM
    {
        [Key] public byte Id { get; set; }
        public string AuthorityName { get; set; }
    }
}
