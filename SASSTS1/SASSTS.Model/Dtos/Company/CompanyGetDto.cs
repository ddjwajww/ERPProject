using Infrastructure.Models;

namespace SASSTS.Model.Dtos.Company
{
    public class CompanyGetDto : IDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNo { get; set; }
    }
}
