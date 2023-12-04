using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.CompanyVM
{
    public class DeleteCompanyVM
    {
        [Key] public byte Id { get; set; }
    }
}
