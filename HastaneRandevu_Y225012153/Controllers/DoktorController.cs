using HastaneRandevu_Y225012153.Data;
using HastaneRandevu_Y225012153.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HastaneRandevu_Y225012153.Controllers
{
    public class DoktorController : Controller
    {
        private HastaneDbContext k = new();

        [HttpGet]
        public IActionResult DoktorListele()
        {
            var HDoktorlar = k.Doktorlar.ToList();
            return View(HDoktorlar);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Doktor doktor)
        {
            if(ModelState.IsValid)
            {
                k.Add(doktor);
                k.SaveChanges();
                return RedirectToAction("DoktorListele");
            }
            else
            {
                ViewBag.msg = "Doktor Eklenemedi";
                return View(doktor);
            }
            

            
        }
    }
}
