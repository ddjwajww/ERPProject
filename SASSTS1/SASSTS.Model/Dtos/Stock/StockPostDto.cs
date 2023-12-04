using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Model.Dtos.Stock
{
    public class StockPostDto : IDto
    {
        public int ProductId { get; set; }
        public DateTime CreateTime { get; set; }
        public int Quantity { get; set; }
        public byte CompanyId { get; set; }
    }
}
