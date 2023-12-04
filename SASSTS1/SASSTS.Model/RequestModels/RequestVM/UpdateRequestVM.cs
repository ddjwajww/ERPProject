using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.RequestVM
{
    public class UpdateRequestVM
    {
        [Key] public long Id { get; set; }
        public long EmployeeId { get; set; }
        public int BasketId { get; set; }
        public string RequestNo { get; set; }
        public byte RequestStatus { get; set; }
    }
}
