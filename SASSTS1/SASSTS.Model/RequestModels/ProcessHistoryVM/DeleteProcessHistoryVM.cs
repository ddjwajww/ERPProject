using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.ProcessHistoryVM
{
    public class DeleteProcessHistoryVM
    {
        [Key] public long Id { get; set; }
    }
}
