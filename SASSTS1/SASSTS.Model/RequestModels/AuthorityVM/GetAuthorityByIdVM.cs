using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.AuthorityVM
{
    public class GetAuthorityByIdVM
    {
        [Key] public byte Id { get; set; }
    }
}
