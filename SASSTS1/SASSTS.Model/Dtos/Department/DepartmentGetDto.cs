using Infrastructure.Models;

namespace SASSTS.Model.Dtos.Department
{
    public class DepartmentGetDto : IDto
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentNo { get; set; }
    }
}
