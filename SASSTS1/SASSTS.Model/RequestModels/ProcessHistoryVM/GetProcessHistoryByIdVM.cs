using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.ProcessHistoryVM
{
    public class GetProcessHistoryByIdVM
    {
        [Key] public long Id { get; set; }
    }
}
