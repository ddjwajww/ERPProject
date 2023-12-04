using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SASSTS.Model.Dtos.Product
{
    public class ProductPostDto : IDto
    {
        public int CategoryId { get; set; }
        public int UnitId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public bool IsDeleted { get; set; }
    }
}
