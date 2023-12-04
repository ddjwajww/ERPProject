using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.BasketDetailVM
{
    public class GetBasketDetailByIdVM
    {
        [Key] public long Id { get; set; }
    }
}
