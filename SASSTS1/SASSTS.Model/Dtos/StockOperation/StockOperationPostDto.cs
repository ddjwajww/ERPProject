using Infrastructure.Models;

namespace SASSTS.Model.Dtos.StockOperation
{
    public class StockOperationPostDto : IDto
    {
        public int Quantity { get; set; }
        public long EmployeeId { get; set; }
        public int StockId { get; set; }
    }
}
