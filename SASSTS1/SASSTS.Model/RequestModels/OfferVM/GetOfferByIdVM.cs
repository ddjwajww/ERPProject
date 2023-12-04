using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.OfferVM
{
    public class GetOfferByIdVM
    {
        [Key] public int Id { get; set; }
    }
}
