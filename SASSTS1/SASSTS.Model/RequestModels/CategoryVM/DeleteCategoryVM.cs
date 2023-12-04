using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.CategoryVM
{
    public class DeleteCategoryVM
    {
        [Key] public byte Id { get; set; }
    }
}
