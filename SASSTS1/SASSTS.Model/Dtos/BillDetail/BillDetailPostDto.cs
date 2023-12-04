using Infrastructure.Models;

namespace SASSTS.Model.Dtos.BillDetail
{
    public class BillDetailPostDto : IDto
    {
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int ProductKdv { get; set; }
    }
}
