using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.Entities
{
    public class ProductImage : IEntity
    {
        [Key] public int Id { get; set; }
        public int ProductId { get; set; }
        public string Path { get; set; }
        public Product Product { get; set; }
    }
}
