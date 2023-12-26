using HastaneRandevuSistemi.Data;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Controllers
{
    public class AdminController : Controller
    {
        private HastaneDbContext _context = new HastaneDbContext();

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AnaBilimDaliEkle()
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

        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Create(AnaBilimDali Dal1) // poliklinik mi olacak burasi
        {
            if (ModelState.IsValid)
            {
                _context.Add(Dal1);
                _context.SaveChanges();
                return RedirectToAction("AnaBilimDaliEkle");
            }
            else
            {
                ViewBag.msj = "Anabilim Dalı Eklenemedi!";
                return View(Dal1);
            }

        }

        public IActionResult Delete(int Id) // Anabilim Dalı Silme
        {
            var AnaBilimDaliD = _context.AnaBilimDallari.Find(Id);
            _context.AnaBilimDallari.Remove(AnaBilimDaliD);
            _context.SaveChanges();

            return RedirectToAction("AnaBilimDaliEkle");
        }

        public IActionResult PoliklinikEkle()
        {
            var admin = true;
            if (admin) // Burada Admin kontrolunu yap
            {
                var PolikliniklerD = _context.Poliklinikler.ToList();
                return View(PolikliniklerD);

            }
            else
                return RedirectToAction("Index", "Home");
        }

        public IActionResult CreatePoliklinik()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreatePoliklinik(Poliklinikler pol1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pol1);
                _context.SaveChanges();
                return RedirectToAction("PoliklinikEkle");
            }
            else
            {
                ViewBag.msj = "Poliklinik  Eklenemedi!";
                return View(pol1);
            }
        }
    }
}
