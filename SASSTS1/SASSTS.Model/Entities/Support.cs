using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Model.Entities
{
    public class Support : IEntity
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
