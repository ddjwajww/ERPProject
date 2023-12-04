using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.RequestModels.AccountingVM
{
    public class UpdateUserVM : IDto
    {
        [Key] public long Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string Phone { get; set; }

    }
}
