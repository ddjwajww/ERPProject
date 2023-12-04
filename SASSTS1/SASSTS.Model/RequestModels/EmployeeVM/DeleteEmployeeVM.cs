using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.EmployeeVM
{
    public class DeleteEmployeeVM
    {
        [Key] public long Id { get; set; }
    }
}
