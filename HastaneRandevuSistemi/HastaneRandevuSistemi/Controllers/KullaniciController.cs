 using HastaneRandevuSistemi.Data;
using HastaneRandevuSistemi.Models;
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

        private HastaneDbContext _context = new HastaneDbContext();

        public IActionResult Index()
        {
            return RedirectToAction("Randevularim", "Kullanici");
        }

        public IActionResult Poliklinikler()
        {
            var poliklinikler = _context.Poliklinikler.ToList();
            var AllAnabilimDallari = _context.AnaBilimDallari.ToList();

            var poliklinikList1 = new List<AnaBilimDaliBilgileri>();

            foreach (var poliklinik in poliklinikler)
            {
                var anaBilimDali = AllAnabilimDallari.FirstOrDefault(anabilimD => anabilimD.Id == poliklinik.AnaBilimDaliId);

                var poliklinikList2 = new AnaBilimDaliBilgileri
                {
                    poliklinikler = poliklinik,
                    AnaBilimDallariAdi = anaBilimDali?.AnaBilimDaliAdi
                };

                poliklinikList1.Add(poliklinikList2);
            }

            return View(poliklinikList1);
        }

        public IActionResult Doktorlar()
        {
            var AllDoktorlar = _context.Doktorlar.ToList();
            var AllPoliklinikler = _context.Poliklinikler.ToList();

            var poliklinikModelList = new List<PoliklinikBilgileri>();

            foreach (var doktor in AllDoktorlar)
            {
                var poliklinikler = AllPoliklinikler.FirstOrDefault(anabilimD => anabilimD.Id == doktor.PoliklinikId);

                var poliklinikBlgi = new PoliklinikBilgileri
                {
                    doktor = doktor,
                    PolikliniklerinAdi = poliklinikler?.PoliklinikAdi
                };

                poliklinikModelList.Add(poliklinikBlgi);
            }

            return View(poliklinikModelList);
        }
        [HttpGet]
        public IActionResult DoktorSec(int Id)
        {
            var CalismaSaatiDoktor = _context.CalismaSaatleri.Where(x => x.DoktorId == Id)
                .ToList();

            if (CalismaSaatiDoktor == null || CalismaSaatiDoktor.Count == 0)
            {
                ViewBag.Mesaj = "Doktorun uygun randevusu bulunamadı.";
            }
            else
            {
                var model = new Tuple<List<CalismaSaatleri>, int>(CalismaSaatiDoktor, Id);
                return View(model);
            }
            return View(); 
        }

        public Doktor DoktorBilgiGetir(int Id)
        {
            var SecilenDoktor = _context.Doktorlar.Where(x => x.Id == Id).FirstOrDefault();
            return SecilenDoktor;
        }
        [HttpPost]
        public IActionResult RandevuOlustur(int doctorId, string selectedCardDate)
        {
            var AktifKullanici = _context.Kullanicilar.FirstOrDefault(u => u.KullaniciAdi == User.Identity.Name);

            var SecilenDoktor = DoktorBilgiGetir(doctorId);
            DateTime randevuSaati = DateTime.ParseExact(selectedCardDate, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);

            var DoktoruBul = _context.Doktorlar.ToList().Where(secilen => secilen.Id == doctorId).FirstOrDefault();


            var NewRandevu = new Randevu
            {
                KullaniciId = AktifKullanici.Id,
                KullaniciAdi = AktifKullanici.KullaniciAdi,
                RandevuSaati = randevuSaati,
                DoktorAdi = SecilenDoktor.DoktorAdSoyad,
                DoktorId = SecilenDoktor.Id
            };

            _context.Randevular.Add(NewRandevu);

            _context.SaveChanges();

            return RedirectToAction("Index", "Kullanici");
        }

        [Authorize]
        [HttpGet]
        public IActionResult RandevuAl()
        {
            var Listpoliklinik = _context.Poliklinikler.Select(gelendeger => new SelectListItem
            {
                Value = gelendeger.Id.ToString(),
                Text = gelendeger.PoliklinikAdi
            }).ToList();

            return View(Listpoliklinik);
        }

        [HttpPost]
        public IActionResult RandevuAl(IFormCollection form)
        {
            int selectPoliId = Convert.ToInt32(form["id"]);

            var DoktorPoliklinik = _context.Doktorlar
                .Where(x => x.PoliklinikId == selectPoliId)
                .Select(doktorD => new SelectListItem
                {
                    Value = doktorD.Id.ToString(),
                    Text = doktorD.DoktorAdSoyad
                })
                .ToList();

            ViewBag.Doktorlar = DoktorPoliklinik;

            var Listpoliklinik = _context.Poliklinikler.Select(kullaniciD => new SelectListItem
            {
                Value = kullaniciD.Id.ToString(),
                Text = kullaniciD.PoliklinikAdi
            }).ToList();

            return View(Listpoliklinik);
        }

        public IActionResult RandevuSil(int Id)
        {
            var Deleterandevu = _context.Randevular.Find(Id);
            _context.Remove(Deleterandevu);
            _context.SaveChanges();
            return RedirectToAction("Randevularim", "Kullanici");
        }

        public IActionResult Randevularim()
        {
            var AktifKullanici = _context.Randevular.Where(randevu => randevu.KullaniciAdi == User.Identity.Name).ToList();
            return View(AktifKullanici);
        }
    }


}
