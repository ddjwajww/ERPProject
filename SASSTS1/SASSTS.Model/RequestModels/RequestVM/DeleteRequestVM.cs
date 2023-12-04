using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.RequestVM
{
    public class DeleteRequestVM
    {
        [Key] public long Id { get; set; }
    }
}
