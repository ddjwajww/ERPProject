using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Model.Dtos.Unit
{
    public class UnitPostDto : IDto
    {
        public string UnitName { get; set; }
    }
}
