using Infrastructure.Models;

namespace SASSTS.Model.Dtos.Accounting
{
    public class AccountingGetDto : IDto
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public string Image { get; set; }
    }
}
