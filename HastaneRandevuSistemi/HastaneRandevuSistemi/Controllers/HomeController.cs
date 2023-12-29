using HastaneRandevuSistemi.Data;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Security.Claims;

namespace HastaneRandevuSistemi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        HastaneDbContext _context = new HastaneDbContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult KayitOl()
        {
            return View();
        }
        [HttpPost]
        public IActionResult KayitOl(Kullanicilar YKullanici)
        {
            var YeniKullanici = new Kullanicilar
            {
                KullaniciAdSoyad = YKullanici.KullaniciAdSoyad,
                KullaniciAdi = YKullanici.KullaniciAdi,
                KullaniciSifre = YKullanici.KullaniciSifre,
                KullaniciEmail = YKullanici.KullaniciEmail,
            };

            _context.Kullanicilar.Add(YeniKullanici);
            _context.SaveChanges();
            return RedirectToAction("GirisYap");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GirisYap(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                TempData["ReturnUrl"] = returnUrl;
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GirisYapAsync(Kullanicilar user)
        {
            var kontrol = _context.Kullanicilar.FirstOrDefault(x => x.KullaniciAdi == user.KullaniciAdi && x.KullaniciSifre == user.KullaniciSifre);
            if (kontrol != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.KullaniciAdi)
                };
                var userIdentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                string returnUrl = TempData["ReturnUrl"] as string;
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }

                //Burası güncellenecek öğrenci numarasına göre
                if (kontrol.KullaniciAdi == "Y225012153@sakarya.edu.tr")
                    return RedirectToAction("Index", "Admin");
                else
                    return RedirectToAction("Index", "Kullanici");
            }
            else
            {
                ViewBag.Message = "Kullanıcı adı veya şifre hatalı.";
                return View();
            }

        }
    }
}
