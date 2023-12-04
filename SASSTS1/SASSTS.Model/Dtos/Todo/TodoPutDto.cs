using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Model.Dtos.Todo
{
    public class TodoPutDto : IDto
    {
        public int Id { get; set; }
        public bool Active { get; set; }
    }
}
