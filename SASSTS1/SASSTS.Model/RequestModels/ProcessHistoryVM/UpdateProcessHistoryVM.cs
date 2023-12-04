using SASSTS.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.ProcessHistoryVM
{
    public class UpdateProcessHistoryVM
    {
        [Key] public long Id { get; set; }
        public long EmployeeId { get; set; }
        public DateTime ProcessTime { get; set; }
        public string ProcessType { get; set; }
        public string ProcessDetail { get; set; }
    }
}
