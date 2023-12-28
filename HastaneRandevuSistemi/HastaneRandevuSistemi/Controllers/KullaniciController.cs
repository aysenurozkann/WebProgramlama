using HastaneRandevuSistemi.Data;
using HastaneRandevuSistemi.Models;
using HastaneRandevuSistemi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Globalization;

namespace HastaneRandevuSistemi.Controllers
{
    public class KullaniciController : Controller
    {
        private LanguageService _localization;

        private HastaneDbContext _context = new HastaneDbContext();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Poliklinikler()
        {
            var poliklinikler = _context.Poliklinikler.ToList();
            var tumAnabilimDallari = _context.AnaBilimDallari.ToList();

            var poliklinikList1 = new List<AnaBilimDaliBilgileri>();

            foreach (var poliklinik in poliklinikler)
            {
                var anaBilimDali = tumAnabilimDallari.FirstOrDefault(abd => abd.Id == poliklinik.AnaBilimDaliId);

                var poliklinikList2 = new AnaBilimDaliBilgileri
                {
                    poliklinikler = poliklinik,
                    AnaBilimDallariAdi = anaBilimDali?.AnaBilimDaliAdi
                };

                poliklinikList1.Add(poliklinikList2);
            }

            return View(poliklinikList1);
        }
        public String GetEslesenVeri(int anaBilimDaliId)
        {
            var eslesenAnaBilimDali = _context.AnaBilimDallari.FirstOrDefault(x => x.Id == anaBilimDaliId);
            if (eslesenAnaBilimDali != null)
            {
                return eslesenAnaBilimDali.AnaBilimDaliAdi;
            }
            else
            {
                return "Eşleşen veri bulunamadı";
            }
        }

        public IActionResult DoktorlarVePoliklinik(int anaBilimDaliId)
        {
            var tumDoktorlar = _context.Doktorlar.ToList();
            var tumPoliklinikler = _context.Poliklinikler.ToList();

            var doktorlarBuAnabilimDalinda = tumDoktorlar.Where(d => tumPoliklinikler.Any(p => p.AnaBilimDaliId == anaBilimDaliId && p.Id == d.PoliklinikId)).ToList();

            var poliklinikList = new List<PoliklinikBilgileri>();

            foreach (var doktor in doktorlarBuAnabilimDalinda)
            {
                var poliklinikler = tumPoliklinikler.FirstOrDefault(abd => abd.Id == doktor.PoliklinikId);

                var doktorVePoliklinik = new PoliklinikBilgileri
                {
                    doktor = doktor,
                    PolikliniklerinAdi = poliklinikler?.PoliklinikAdi
                };

                poliklinikList.Add(doktorVePoliklinik);
            }

            return View(poliklinikList);
        }

        public IActionResult Doktorlar()
        {
            var tumDoktorlar = _context.Doktorlar.ToList();
            var tumPoliklinikler = _context.Poliklinikler.ToList();

            var poliklinikModelList = new List<PoliklinikBilgileri>();

            foreach (var doktor in tumDoktorlar)
            {
                var poliklinikler = tumPoliklinikler.FirstOrDefault(abd => abd.Id == doktor.PoliklinikId);

                var doktorVePoliklinik = new PoliklinikBilgileri
                {
                    doktor = doktor,
                    PolikliniklerinAdi = poliklinikler?.PoliklinikAdi
                };

                poliklinikModelList.Add(doktorVePoliklinik);
            }

            return View(poliklinikModelList);
        }

        [HttpGet]
        public IActionResult RandevuAl()
        {
            var poliklinikListesi = _context.Poliklinikler.Select(h => new SelectListItem
            {
                Value = h.Id.ToString(),
                Text = h.PoliklinikAdi
            }).ToList();

            return View(poliklinikListesi);
        }

        [HttpPost]
        public IActionResult RandevuAl(IFormCollection form)
        {
            int selectedPoliklinikId = Convert.ToInt32(form["id"]);

            var poliklinigeGoreDoktorlar = _context.Doktorlar
                .Where(x => x.PoliklinikId == selectedPoliklinikId)
                .Select(doktorD => new SelectListItem
                {
                    Value = doktorD.Id.ToString(),
                    Text = doktorD.DoktorAdSoyad
                })
                .ToList();

            ViewBag.Doktorlar = poliklinigeGoreDoktorlar;

            var poliklinikListesi = _context.Poliklinikler.Select(kullaniciD => new SelectListItem
            {
                Value = kullaniciD.Id.ToString(),
                Text = kullaniciD.PoliklinikAdi
            }).ToList();

            return View(poliklinikListesi);
        }

        //public String GetEslesenVeri(int anaBilimDaliId)
        //{
        //    var eslesenAnaBilimDali = _context.AnaBilimDallari.FirstOrDefault(x => x.Id == anaBilimDaliId);
        //    if (eslesenAnaBilimDali != null)
        //    {
        //        return eslesenAnaBilimDali.AnaBilimDaliAdi;
        //    }
        //    else
        //    {
        //        return "Eşleşen veri bulunamadı";
        //    }
        //}

        //public IActionResult DoktorVePoliklinikBilgisi(int anaBilimDaliId)
        //{
        //    var tumDoktorlar = _context.Doktorlar.ToList();
        //    var tumPoliklinikler = _context.Poliklinikler.ToList();

        //    var doktorlarBuAnabilimDalinda = tumDoktorlar.Where(d => tumPoliklinikler.Any(p => p.AnaBilimDaliId == anaBilimDaliId && p.Id == d.PoliklinikId)).ToList();

        //    var poliklinikModelList = new List<PoliklinikBilgileri>();

        //    foreach (var doktor in doktorlarBuAnabilimDalinda)
        //    {
        //        var poliklinikler = tumPoliklinikler.FirstOrDefault(abd => abd.Id == doktor.PoliklinikId);

        //        var doktorVePoliklinik = new PoliklinikBilgileri()
        //        {
        //            doktor = doktor,
        //            PolikliniklerinAdi = poliklinikler?.PoliklinikAdi
        //        };

        //        poliklinikModelList.Add(doktorVePoliklinik);
        //    }

        //    return View(poliklinikModelList);
        //}

        public IActionResult ChangeLanguage(string culture, string returnUrl)
        {
            if (!string.IsNullOrEmpty(culture))
            {
                // Seçilen dili, tarayıcı çerezlerine kaydedelim
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            }

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                // returnUrl boş değilse ve güvenli bir yerel URL ise o sayfaya yönlendir
                return LocalRedirect(returnUrl);
            }
            else
            {
                // returnUrl boş veya güvenli bir yerel URL değilse varsayılan olarak Anasayfa'ya yönlendir
                return RedirectToAction("Index", "Home");
            }
        }


    }


}
