using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Model.Dtos.Company
{
    public class CompanyPutDto : IDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNo { get; set; }
        public bool IsDeleted { get; set; }
    }
}
