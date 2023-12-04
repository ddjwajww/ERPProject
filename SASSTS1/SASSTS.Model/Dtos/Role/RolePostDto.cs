using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Model.Dtos.Role
{
    public class RolePostDto : IDto
    {
        public string RoleName { get; set; }
    }
}
