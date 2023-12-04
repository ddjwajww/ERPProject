namespace SatinAlmaStokYonetim.Web.Models.Report
{
    public class TimeReportGetDto
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime SuccessDate { get; set; }
        public DateTime SADate { get; set; }
        public DateTime ManagementDate { get; set; }
        public DateTime AccountDate { get; set; }
        public DateTime StockDate { get; set; }
        public int CompanyId { get; set; }
        public bool? IsRead { get; set; }
    }
}
