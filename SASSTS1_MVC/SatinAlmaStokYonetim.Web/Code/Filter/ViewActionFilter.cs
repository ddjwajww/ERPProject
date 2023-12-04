using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.Code;

namespace SatinAlmaStokYonetim.Web.Code.Filter
{
    public class ViewActionFilter : ActionFilterAttribute, IAuthorizationFilter
    {
        public string? ViewPageName;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!string.IsNullOrEmpty(ViewPageName))
            {
                bool isAuthorized = false;
                switch (ViewPageName)
                {
                    case "RequestList":
                        if (Repo.Session.Authority == "Talep" || Repo.Session.Authority == "Birim Amiri" || Repo.Session.Authority == "SuperUser")
                        {
                            isAuthorized = true;
                        }
                        break;

                    case "CreateRequest":
                        if (Repo.Session.Authority == "Talep" || Repo.Session.Authority == "SuperUser")
                        {
                            isAuthorized = true;
                        }
                        break;
                    case "CreateSupplier":
                        if (Repo.Session.Authority == "Satın Alma" || Repo.Session.Authority == "SuperUser")
                        {
                            isAuthorized = true;
                        }
                        break;
                    case "ApproveRequest":
                        if (Repo.Session.Authority == "Birim Amiri" || Repo.Session.Authority == "SuperUser")
                        {
                            isAuthorized = true;
                        }
                        break;
                    case "CreateOffer":
                        if (Repo.Session.Authority == "Satın Alma" || Repo.Session.Authority == "SuperUser")
                        {
                            isAuthorized = true;
                        }
                        break;
                    case "OfferApproval":
                        if (Repo.Session.Authority == "Kurul" || Repo.Session.Authority == "Yönetim" || Repo.Session.Authority == "SuperUser")
                        {
                            isAuthorized = true;
                        }
                        break;
                    case "ManageStock":
                        if (Repo.Session.Authority == "Satın Alma" || Repo.Session.Authority == "SuperUser")
                        {
                            isAuthorized = true;
                        }
                        break;
                    case "Supplier":
                        if (Repo.Session.Authority == "Satın Alma" || Repo.Session.Authority == "SuperUser")
                        {
                            isAuthorized = true;
                        }
                        break;
                    case "InvoiceEntry":
                        if (Repo.Session.Authority == "Muhasebe" || Repo.Session.Authority == "SuperUser")
                        {
                            isAuthorized = true;
                        }
                        break;
                    case "Invoices":
                        if (Repo.Session.Authority == "Muhasebe" || Repo.Session.Authority == "SuperUser")
                        {
                            isAuthorized = true;
                        }
                        break;

                    case "Reports":
                        if (Repo.Session.Authority == "Kurul" || Repo.Session.Authority == "Yönetim" || Repo.Session.Authority == "SuperUser")
                        {
                            isAuthorized = true;
                        }
                        break;
                    case "TimeReports":
                        if (Repo.Session.Authority == "Kurul" || Repo.Session.Authority == "Yönetim" || Repo.Session.Authority == "SuperUser")
                        {
                            isAuthorized = true;
                        }
                        break;
                }
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
