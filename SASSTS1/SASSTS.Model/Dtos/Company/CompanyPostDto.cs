using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Model.Dtos.Company
{
    public class CompanyPostDto : IDto
    {
        public string CompanyName { get; set; }
        public string CompanyNo { get; set; }
    }
}
