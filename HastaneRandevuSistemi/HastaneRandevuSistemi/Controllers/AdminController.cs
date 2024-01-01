using HastaneRandevuSistemi.Data;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace HastaneRandevuSistemi.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private HastaneDbContext _context = new HastaneDbContext();

        public bool AdminKontrol()
        {
            var admin = true;
            if (admin)
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
                var AllAnabilimDallari = _context.AnaBilimDallari.ToList();

                var poliklinikList1 = new List<AnaBilimDaliBilgileri>();

                foreach (var poliklinik in poliklinikler)
                {
                    var anabilimDali = AllAnabilimDallari.FirstOrDefault(abd => abd.Id == poliklinik.AnaBilimDaliId);

                    var poliklinikList2 = new AnaBilimDaliBilgileri
                    {
                        poliklinikler = poliklinik,
                        AnaBilimDallariAdi = anabilimDali?.AnaBilimDaliAdi
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
                var anabilimDaliListesi = _context.AnaBilimDallari.Select(ekle => new SelectListItem
                {
                    Value = ekle.Id.ToString(),
                    Text = ekle.AnaBilimDaliAdi
                }).ToList();

                return View(anabilimDaliListesi);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult PoliklinikEkle(IFormCollection form) // FormCollection
        {
            

            string Gelenname = form["PoliklinikAdi"];
            int AddAnaBilimDaliId = Convert.ToInt32(form["id"]);

            using (var dbContext = new HastaneDbContext())
            {
                Poliklinikler NewPoliklinik = new Poliklinikler
                {
                    PoliklinikAdi = Gelenname,
                    AnaBilimDaliId = AddAnaBilimDaliId
                };

                dbContext.Poliklinikler.Add(NewPoliklinik);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Poliklinik", "Admin");
        }

        public IActionResult PoliklinikSil(int Id)
        {
            var Deletepoliklinik = _context.Poliklinikler.Find(Id);
            _context.Poliklinikler.Remove(Deletepoliklinik);
            _context.SaveChanges();

            return RedirectToAction("Poliklinik", "Admin");
        }

        public IActionResult Doktor()
        {
            if (AdminKontrol())
            {
                var AllDoktorlar = _context.Doktorlar.ToList();
                var AllPoliklinikler = _context.Poliklinikler.ToList();

                var poliklinikList1 = new List<PoliklinikBilgileri>();

                foreach (var doktor in AllDoktorlar)
                {
                    var poliklinikler = AllPoliklinikler.FirstOrDefault(poli => poli.Id == doktor.PoliklinikId);

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
                var Listpoliklinik = _context.Poliklinikler.Select(poli => new SelectListItem
                {
                    Value = poli.Id.ToString(),
                    Text = poli.PoliklinikAdi
                }).ToList();

                return View(Listpoliklinik);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult DoktorEkle(IFormCollection form) // FormCollection
        {
            

            string Gelenname = form["PoliklinikAdi"];
            int AddpoliklinikId = Convert.ToInt32(form["id"]);

            using (var dbContext = new HastaneDbContext())
            {
                Doktor NewDoktor = new Doktor
                {
                    DoktorAdSoyad = Gelenname,
                    PoliklinikId = AddpoliklinikId
                };

                dbContext.Doktorlar.Add(NewDoktor);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Doktor", "Admin");
        }

        public IActionResult DoktorSil(int Id)
        {
            var Deletedoktor = _context.Doktorlar.Find(Id);
            _context.Doktorlar.Remove(Deletedoktor);
            _context.SaveChanges();

            return RedirectToAction("Doktor", "Admin");
        }

        [HttpPost]
        public IActionResult CalismaSaatiAta(int Id, string CalismaSaati)
        {


            var doktor = _context.Doktorlar.Find(Id);

            if (doktor != null)
            {
                DateTime DoktorCalismaSaati = DateTime.ParseExact(CalismaSaati, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);

                var NewCalismaSaati = new CalismaSaatleri
                {
                    DoktorId = doktor.Id,
                    CalismaSaati = DoktorCalismaSaati,
                    DoktorAdi = doktor.DoktorAdSoyad
                };

                _context.CalismaSaatleri.Add(NewCalismaSaati);
                _context.SaveChanges();

                return RedirectToAction("Doktor", "Admin");
            }
            else
            {

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
            var DeleteRandevu = _context.Randevular.Find(Id);

            if (DeleteRandevu != null)
            {
                _context.Remove(DeleteRandevu);
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
