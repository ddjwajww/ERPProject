using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Model.Dtos.Category
{
    public class CategoryPostDto : IDto
    {
        public string CategoryName { get; set; }
    }
}
