using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.StockVM
{
    public class GetStockByIdVM
    {
        [Key] public int Id { get; set; }
    }
}
