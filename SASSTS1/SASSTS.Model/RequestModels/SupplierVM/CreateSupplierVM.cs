using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.SupplierVM
{
    public class CreateSupplierVM
    {
        public string CompanyMail { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPhone { get; set; }
    }
}
