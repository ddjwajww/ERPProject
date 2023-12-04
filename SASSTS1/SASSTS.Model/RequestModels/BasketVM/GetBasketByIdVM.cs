using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.BasketVM
{
    public class GetBasketByIdVM
    {
        [Key] public int Id { get; set; }
    }
}
