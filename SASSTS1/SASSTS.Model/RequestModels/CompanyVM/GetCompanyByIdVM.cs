using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.CompanyVM
{
    public class GetCompanyByIdVM
    {
        [Key] public byte Id { get; set; }
    }
}
