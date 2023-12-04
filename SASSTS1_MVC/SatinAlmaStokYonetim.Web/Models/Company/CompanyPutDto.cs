namespace SatinAlmaStokYonetim.Web.Models.Company
{
    public class CompanyPutDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNo { get; set; }
        public bool isDeleted { get; set; }

    }
}
