using SatinAlmaStokYonetim.Web.Code;

namespace SatinAlmaStokYonetim.Web.Models.Support
{
    public class SupportPostDto
    {
        private int _employeeId = Convert.ToInt32(Repo.Session.UserId);
        public int EmployeeId
        {
            get
            {
                return _employeeId;
            }
            set
            {
                _employeeId = value;
            }
        }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
