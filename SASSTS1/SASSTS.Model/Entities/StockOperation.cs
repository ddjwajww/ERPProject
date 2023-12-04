using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.Entities
{
    public class StockOperation : IEntity
    {
        [Key] public int Id { get; set; }
        public int Quantity { get; set; }
        public bool Status { get; set; }
        public DateTime OperationTime { get; set; }
        public long EmployeeId { get; set; }
        public int StockId { get; set; }
        public Employee Employee { get; set; }
        public Stock Stock { get; set; }
    }
}
