using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.Code.Filter;
using SatinAlmaStokYonetim.Web.Models;
using System.Diagnostics;

namespace SatinAlmaStokYonetim.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    [AuthActionFilter(Role = "Personel")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index() => View();
        public IActionResult Message() => View();
        public IActionResult History() => View();

        [ViewActionFilter(ViewPageName = "CreateRequest")]
        public IActionResult CreateRequest() => View();

        [ViewActionFilter(ViewPageName = "RequestList")]
        public IActionResult RequestList() => View();

        [ViewActionFilter(ViewPageName = "ApproveRequest")]
        public IActionResult ApproveRequest() => View();

        [ViewActionFilter(ViewPageName = "OfferApproval")]
        public IActionResult OfferApproval() => View();

        [ViewActionFilter(ViewPageName = "CreateOffer")]
        public IActionResult CreateOffer() => View();

        [ViewActionFilter(ViewPageName = "ManageStock")]
        public IActionResult ManageStock() => View();

        [ViewActionFilter(ViewPageName = "Supplier")]
        public IActionResult Supplier() => View();

        [ViewActionFilter(ViewPageName = "Invoices")]
        public IActionResult Invoices() => View();

        [ViewActionFilter(ViewPageName = "CreateSupplier")]
        public IActionResult CreateSupplier() => View();

        [ViewActionFilter(ViewPageName = "InvoiceEntry")]
        public IActionResult InvoiceEntry() => View();

        [ViewActionFilter(ViewPageName = "Reports")]
        public IActionResult Reports() => View();

        [ViewActionFilter(ViewPageName = "TimeReports")]
        public IActionResult TimeReports() => View();
        public IActionResult Null() => View();
        public IActionResult Account() => View();
        public IActionResult Support() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}