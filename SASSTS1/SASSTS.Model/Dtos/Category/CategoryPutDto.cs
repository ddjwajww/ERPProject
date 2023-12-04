using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Model.Dtos.Category
{
    public class CategoryPutDto : IDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}
