using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.RequestVM
{
    public class GetRequestByIdVM
    {
        [Key] public long Id { get; set; }
    }
}
