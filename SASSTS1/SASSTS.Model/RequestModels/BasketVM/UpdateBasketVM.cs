using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.BasketVM
{
    public class UpdateBasketVM
    {
        [Key] public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
