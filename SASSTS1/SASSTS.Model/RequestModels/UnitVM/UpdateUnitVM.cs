using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.UnitVM
{
    public class UpdateUnitVM
    {
        [Key] public byte Id { get; set; }
        public string UnitName { get; set; }
    }
}
