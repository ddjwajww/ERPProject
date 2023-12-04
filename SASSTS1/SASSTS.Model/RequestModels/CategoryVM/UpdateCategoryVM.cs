using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.CategoryVM
{
    public class UpdateCategoryVM
    {
        [Key] public byte Id { get; set; }
        public string CategoryName { get; set; }
    }
}
