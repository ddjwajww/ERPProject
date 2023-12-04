using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Model.Dtos.Employee
{
    public class EmployeeImageDto
    {
        public int Id { get; set; }
        public long EmployeeId { get; set; }
        public string Path { get; set; }
    }
}
