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
        public IActionResult DoktorSec(int Id)
        {
            var doktorBilgileri = _context.Doktorlar.Where(x => x.Id == Id).ToList();
            return View();
        }

        public Doktor DoktorBilgiGetir(int Id)

        {
            var RandevuAlinanDoktor = _context.Doktorlar.Where(x=> x.Id == Id).FirstOrDefault();
            return RandevuAlinanDoktor;
        }
        [HttpPost]
        public IActionResult RandevuOlustur(int doctorId)
        {
            var currentUser = _context.Kullanicilar.FirstOrDefault(u => u.KullaniciAdi == User.Identity.Name);

            //randevu alınan doktoru idsine göre bul
            var randevuAlinanDoktor = DoktorBilgiGetir(doctorId);


            //Doktorun id si ve çalışma saati veritabanında varsa yani böyle bir doktor varsa bunu değişkene ata çünk randevu alınca bunu kaldırmamız gerekecek.
            var DoktoruBul = _context.Doktorlar.ToList().Where(x => x.Id == doctorId).FirstOrDefault();


            var yeniRandevu = new Randevu
            {
                KullaniciId = currentUser.Id,
                KullaniciAdi = currentUser.KullaniciAdi,
                RandevuSaati = DateTime.Now,
                DoktorAdi = randevuAlinanDoktor.DoktorAdSoyad,
                DoktorId = randevuAlinanDoktor.Id
            };

            _context.Randevular.Add(yeniRandevu);

            _context.SaveChanges();

            return RedirectToAction("Index", "Kullanici");
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

   


    }


}
