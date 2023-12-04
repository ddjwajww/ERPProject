using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.UnitVM
{
    public class DeleteUnitVM
    {
        [Key] public byte Id { get; set; }
    }
}
