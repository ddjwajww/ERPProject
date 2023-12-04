using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.Entities
{
    public class Unit : IEntity
    {
        [Key] public int Id { get; set; }
        public string UnitName { get; set; }
        public bool? IsDeleted { get; set; }
        public List<Product> Products { get; set; }
    }
}