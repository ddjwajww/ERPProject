namespace SASSTS.Model.RequestModels.ProcessHistoryVM
{
    public class CreateProcessHistoryVM
    {
        public long EmployeeId { get; set; }
        public DateTime ProcessTime { get; set; }
        public string ProcessType { get; set; }
        public string ProcessDetail { get; set; }
    }
}
