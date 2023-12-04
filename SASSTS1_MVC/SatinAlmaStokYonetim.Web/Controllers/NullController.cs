using Microsoft.AspNetCore.Mvc;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class NullController : Controller
    {
        public IActionResult Maintenance_de() => View();
        public IActionResult Maintenance_en() => View();
        public IActionResult Maintenance_es() => View();
        public IActionResult Maintenance_it() => View();

    }
}
