using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.DepartmentVM
{
    public class GetDepartmentByIdVM
    {
        [Key] public byte Id { get; set; }
    }
}
