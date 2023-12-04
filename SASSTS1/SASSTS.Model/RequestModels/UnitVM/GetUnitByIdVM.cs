using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.UnitVM
{
    public class GetUnitByIdVM
    {
        [Key] public byte Id { get; set; }
    }
}
