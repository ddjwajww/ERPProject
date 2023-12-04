using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Model.Dtos.Product
{
    public class ProductPutDto : IDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int UnitId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public bool IsDeleted { get; set; }
    }
}
