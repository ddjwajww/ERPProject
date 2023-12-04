using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.CompanyVM
{
    public class UpdateCompanyVM
    {
        [Key] public byte Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNo { get; set; }
    }
}
