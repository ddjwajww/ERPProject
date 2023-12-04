using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Model.Dtos.Accounting
{
    public class EmployeeandAccountModel:  IDto
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public string UserPassword { get; set; }
        public string? UserName { get; set; }
        public string Image { get; set; }
    }
}
