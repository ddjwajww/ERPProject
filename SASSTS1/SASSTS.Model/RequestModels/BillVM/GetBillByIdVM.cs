using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.BillVM
{
    public class GetBillByIdVM
    {
        [Key] public int Id { get; set; }
    }
}
