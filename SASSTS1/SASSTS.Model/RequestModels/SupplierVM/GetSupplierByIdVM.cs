using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.SupplierVM
{
    public class GetSupplierByIdVM
    {
        [Key] public int Id { get; set; }
    }
}
