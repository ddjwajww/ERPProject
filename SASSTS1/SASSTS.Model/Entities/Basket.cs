using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.Entities
{
    public class Basket : IEntity
    {
        [Key] public int Id { get; set; }
        public DateTime CreatedTime { get; set; }

        public List<BasketDetail> BasketDetails { get; set; }
        public List<Request> Requests { get; set; }
    }
}
