using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.Entities
{
    public class RandomNumber : IEntity
    {
        [Key] public int Id { get; set; }
        public int Number { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
    }
}
