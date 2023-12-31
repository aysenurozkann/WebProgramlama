using HastaneRandevuSistemi.Data;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace HastaneRandevuSistemi.Controllers
{
    public class AdminController : Controller
    {
        private HastaneDbContext _context = new HastaneDbContext();

        public bool AdminKontrol()
        {
            var admin = true;
            if(admin)
            {
                return true;
            }
            else { return false; }
        }
        public IActionResult Index()
        {
            if (AdminKontrol())
            {
                var Anabilimdallari = _context.AnaBilimDallari.ToList();
                return View(Anabilimdallari);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        public IActionResult AnabilimDaliEkle()
        {
            if(AdminKontrol())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult AnabilimDaliEkle(AnaBilimDali AnaBilimDaliD)
        {
            var yeniAnabilimDali = new AnaBilimDali
            {
                AnaBilimDaliAdi = AnaBilimDaliD.AnaBilimDaliAdi
            };
            _context.AnaBilimDallari.Add(yeniAnabilimDali);
            _context.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }

        public IActionResult AnabilimDaliSil(int Id)
        {
            var AnabilimDaliD = _context.AnaBilimDallari.Find(Id);
            _context.AnaBilimDallari.Remove(AnabilimDaliD);
            _context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

        public IActionResult Poliklinik()
        {
            if (AdminKontrol())
            {
                var poliklinikler = _context.Poliklinikler.ToList();
                var TumAnabilimDallari = _context.AnaBilimDallari.ToList();

                var poliklinikList1 = new List<AnaBilimDaliBilgileri>();

                foreach (var poliklinik in poliklinikler)
                {
                    var anaBilimDali = TumAnabilimDallari.FirstOrDefault(abd => abd.Id == poliklinik.AnaBilimDaliId);

                    var poliklinikList2 = new AnaBilimDaliBilgileri
                    {
                        poliklinikler = poliklinik,
                        AnaBilimDallariAdi = anaBilimDali?.AnaBilimDaliAdi
                    };

                    poliklinikList1.Add(poliklinikList2);
                }

                return View(poliklinikList1);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult PoliklinikEkle()
        {
            if (AdminKontrol())
            {
                var anaBilimDaliListesi = _context.AnaBilimDallari.Select(ekle => new SelectListItem
                {
                    Value = ekle.Id.ToString(),
                    Text = ekle.AnaBilimDaliAdi
                }).ToList();

                return View(anaBilimDaliListesi);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult PoliklinikEkle(IFormCollection form)
        {
            // Form verilerini almak için FormCollection nesnesini kullanımı

            string Gelenname = form["PoliklinikAdi"];
            int EklenecekAnaBilimDaliId = Convert.ToInt32(form["id"]);

            using (var dbContext = new HastaneDbContext())
            {
                Poliklinikler yeniPoliklinik = new Poliklinikler
                {
                    PoliklinikAdi = Gelenname,
                    AnaBilimDaliId = EklenecekAnaBilimDaliId
                };

                dbContext.Poliklinikler.Add(yeniPoliklinik);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Poliklinik", "Admin");
        }

        public IActionResult PoliklinikSil(int Id)
        {
            var poliklinik = _context.Poliklinikler.Find(Id);
            _context.Poliklinikler.Remove(poliklinik);
            _context.SaveChanges();

            return RedirectToAction("Poliklinik", "Admin");
        }

        public IActionResult Doktor()
        {
            if (AdminKontrol())
            {
                var TumDoktorlar = _context.Doktorlar.ToList();
                var TumPoliklinikler = _context.Poliklinikler.ToList();

                var poliklinikList1 = new List<PoliklinikBilgileri>();

                foreach (var doktor in TumDoktorlar)
                {
                    var poliklinikler = TumPoliklinikler.FirstOrDefault(dal => dal.Id == doktor.PoliklinikId);

                    var DoktorPoliklinikBilgi = new PoliklinikBilgileri
                    {
                        doktor = doktor,
                        PolikliniklerinAdi = poliklinikler?.PoliklinikAdi
                    };

                    poliklinikList1.Add(DoktorPoliklinikBilgi);
                }

                return View(poliklinikList1);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult DoktorEkle()
        {
            if (AdminKontrol())
            {
                var poliklinikListesi = _context.Poliklinikler.Select(poli => new SelectListItem
                {
                    Value = poli.Id.ToString(),
                    Text = poli.PoliklinikAdi
                }).ToList();

                return View(poliklinikListesi);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult DoktorEkle(IFormCollection form)
        {
            // Form verilerini almak için FormCollection nesnesini kullanımı

            string Gelenname = form["PoliklinikAdi"];
            int EklenecekpoliklinikId = Convert.ToInt32(form["id"]);

            using (var dbContext = new HastaneDbContext())
            {
                Doktor yeniDoktor = new Doktor
                {
                    DoktorAdSoyad = Gelenname,
                    PoliklinikId = EklenecekpoliklinikId
                };

                dbContext.Doktorlar.Add(yeniDoktor);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Doktor", "Admin");
        }

        public IActionResult DoktorSil(int Id)
        {
            var doktor = _context.Doktorlar.Find(Id);
            _context.Doktorlar.Remove(doktor);
            _context.SaveChanges();

            return RedirectToAction("Doktor", "Admin");
        }

        [HttpPost]
        public IActionResult CalismaSaatiAta(int Id, string CalismaSaati)
        {


            var doktor = _context.Doktorlar.Find(Id);

            if (doktor != null)
            {
                DateTime calisma_saati = DateTime.ParseExact(CalismaSaati, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);

                var yeniCalismaSaati = new CalismaSaatleri
                {
                    DoktorId = doktor.Id,
                    CalismaSaati = calisma_saati,
                    DoktorAdi = doktor.DoktorAdSoyad
                };

                _context.CalismaSaatleri.Add(yeniCalismaSaati);
                _context.SaveChanges();

                return RedirectToAction("Doktor", "Admin");
            }
            else
            {
                // Doctor nesnesi null ise burada uygun bir sonuç döndürülmeli

                return RedirectToAction("DoktorCalismaSaatiAta", "Admin");
            }
        }

    
    [HttpGet]
    public IActionResult DoktorCalismaSaatiAta(int Id)
    {
        if (AdminKontrol())
        {
            var doktor = _context.Doktorlar.Find(Id);
            if (doktor != null)
            {
                return View(doktor);
            }
        }
        // Başarısız durumda başka bir sayfaya yönlendirme yapabilirsiniz.
        return RedirectToAction("DoktorCalismaSaatiOlustur", "Admin");
    }

    public IActionResult Randevu()
        {
            if (AdminKontrol())
            {
                var randevular = _context.Randevular.ToList();
                return View(randevular);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult RandevuSil(int Id)
        {
            var silinecekRandevu = _context.Randevular.Find(Id);

            if (silinecekRandevu != null)
            {
                _context.Remove(silinecekRandevu);
                _context.SaveChanges();
                return RedirectToAction("Randevu", "Admin");
            }
            else
            {
                return RedirectToAction("Randevu", "Admin");
            }

        }
    }
}
