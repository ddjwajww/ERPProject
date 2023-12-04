using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.Models;
using SatinAlmaStokYonetim.Web.Services;
using System.Diagnostics;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private LanguageService _localization;

        public HomeController(LanguageService localization, ILogger<HomeController> logger)
        {
            _localization = localization;
            _logger = logger;
        }


        public IActionResult Index()
        {
            ViewBag.Hoşgeldiniz = _localization.Getkey("Hoşgeldiniz").Value;
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            return View();
        }
        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                });
            return Redirect(Request.Headers["Referer"].ToString());
        }
       
        public IActionResult AllRequests() => View();
        public IActionResult ControlRequest() => View();
        public IActionResult CreateRequest() => View(); 
        public IActionResult Product() => View();   
        public IActionResult Message() => View();
        public IActionResult Calender() => View();
        public IActionResult Stock() => View();
        public IActionResult StockDetail() => View();
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}