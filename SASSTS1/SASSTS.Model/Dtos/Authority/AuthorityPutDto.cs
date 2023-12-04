using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Model.Dtos.Authority
{
    public class AuthorityPutDto : IDto
    {
        public int Id { get; set; }
        public string AuthorityName { get; set; }
        public bool isDeleted { get; set; }
    }
}
