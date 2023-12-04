using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.Code;
using SatinAlmaStokYonetim.Web.Models.Account;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class ImageController : Controller
    {

        [HttpPost]
        public IActionResult ChangeImage([FromBody] ImageItem item)
        {
            Repo.Session.Image = item.Image;
            return Json(new { isSuccess = true });
        }
    }
}
