using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.ProductVM
{
    public class GetProductByIdVM
    {
        [Key] public int Id { get; set; }
    }
}
