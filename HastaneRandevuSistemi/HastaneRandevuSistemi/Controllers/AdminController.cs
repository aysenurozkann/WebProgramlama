using HastaneRandevuSistemi.Data;
using Microsoft.AspNetCore.Mvc;

namespace HastaneRandevuSistemi.Controllers
{
    public class AdminController : Controller
    {
        private HastaneDbContext _context = new HastaneDbContext();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AnaBilimDaliGoruntule() 
        {
            var admin = true;
            if (admin) // Burada Admin kontrolunu yap
            {
                var AnaBilimDallariD = _context.AnaBilimDallari.ToList();
                return View(AnaBilimDallariD);

            }
            else
                return RedirectToAction("Index", "Home");
            
            
        }
    }
}
