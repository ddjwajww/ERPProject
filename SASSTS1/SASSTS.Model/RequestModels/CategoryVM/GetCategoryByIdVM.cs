using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.CategoryVM
{
    public class GetCategoryByIdVM
    {
        [Key] public byte Id { get; set; }
    }
}
