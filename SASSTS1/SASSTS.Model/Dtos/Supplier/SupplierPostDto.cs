using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Model.Dtos.Supplier
{
    public class SupplierPostDto : IDto
    {
        public string CompanyMail { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPhone { get; set; }
    }
}
