using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.EmployeeVM
{
    public class GetEmployeeByIdVM
    {
        [Key] public long Id { get; set; }
    }
}
