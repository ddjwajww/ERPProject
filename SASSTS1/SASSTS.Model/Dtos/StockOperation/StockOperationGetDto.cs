using Infrastructure.Models;

namespace SASSTS.Model.Dtos.StockOperation
{
    public class StockOperationGetDto : IDto
    {
        public int Quantity { get; set; }
        public bool Status { get; set; }
        public DateTime OperationTime { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
    }
}
