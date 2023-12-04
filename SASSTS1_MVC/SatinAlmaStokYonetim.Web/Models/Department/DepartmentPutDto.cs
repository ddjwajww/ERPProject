namespace SatinAlmaStokYonetim.Web.Models.Department
{
    public class DepartmentPutDto
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentNo { get; set; }
        public bool isDeleted { get; set; }
    }
}
