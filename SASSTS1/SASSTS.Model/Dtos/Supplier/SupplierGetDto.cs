using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Model.Dtos.Supplier
{
    public class SupplierGetDto : IDto
    {
        public int Id { get; set; }
        public string CompanyMail { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPhone { get; set; }
    }
}
