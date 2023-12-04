using Infrastructure.Models;

namespace SASSTS.Model.Dtos.Request
{
    public class RequestGetDto : IDto
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public long BasketId { get; set; }
        public string RequestNo { get; set; }
        public byte RequestStatus { get; set; }
    }
}
