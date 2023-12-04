using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.DepartmentVM
{
    public class UpdateDepartmentVM
    {
        [Key] public byte Id { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentNo { get; set; }
    }
}
