using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Business.Security.JWT
{
    public class AccessToken
    {
        public List<string>? Claims { get; set; }
        public string? Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
