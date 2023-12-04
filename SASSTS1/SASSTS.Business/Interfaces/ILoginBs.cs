using Azure.Core;
using Infrastructure.ApiResponses;
using SASSTS.Business.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessToken = SASSTS.Business.Security.JWT.AccessToken;

namespace SASSTS.Business.Interfaces
{
    public interface ILoginBs
    {
        Task<ApiResponse<AccessToken>> LoginAsync(string ? email, string ? userName, string userPassword);
    }
}
