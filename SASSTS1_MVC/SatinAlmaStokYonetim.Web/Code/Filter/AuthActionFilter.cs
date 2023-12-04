using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace SatinAlmaStokYonetim.Web.Code.Filter
{
    public class AuthActionFilter : ActionFilterAttribute, IAuthorizationFilter
    {
        public string? Role;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!string.IsNullOrEmpty(Role))
            {
                bool isAuthorized = Role.Split(',').Contains(Repo.Session.Role);
                if (!isAuthorized)
                    context.Result = new UnauthorizedResult();
            }
            else if (string.IsNullOrEmpty(Repo.Session.Name))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
