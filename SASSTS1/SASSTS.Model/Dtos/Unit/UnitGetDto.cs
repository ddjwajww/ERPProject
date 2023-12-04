using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.Dtos.Unit
{
    public class UnitGetDto : IDto
    {
        public int Id { get; set; }
        public string UnitName { get; set; }
    }
}
