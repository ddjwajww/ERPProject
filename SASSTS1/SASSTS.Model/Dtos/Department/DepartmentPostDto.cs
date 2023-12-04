using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Model.Dtos.Department
{
    public class DepartmentPostDto : IDto
    {
        public string DepartmentName { get; set; }
        public string DepartmentNo { get; set; }
    }
}
