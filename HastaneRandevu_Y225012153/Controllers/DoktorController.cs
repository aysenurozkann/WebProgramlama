using Microsoft.AspNetCore.Mvc;

namespace HastaneRandevu_Y225012153.Controllers
{
    public class DoktorController : Controller
    {
        [HttpGet]
        public IActionResult DoktorEkle()
        {
            return View();
        }
    }
}
