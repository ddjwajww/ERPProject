using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Model.Dtos.Todo
{
    public class TodoPostDto : IDto
    {
        public string Text { get; set; }
        public long EmployeeId { get; set; }
        public bool Active { get; set; } = false;
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
    }
}
