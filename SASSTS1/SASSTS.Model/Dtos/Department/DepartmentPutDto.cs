using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Model.Dtos.Department
{
    public class DepartmentPutDto : IDto
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentNo { get; set; }
        public bool IsDeleted { get; set; }
    }
}
