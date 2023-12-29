using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Models
{
    public class Randevu
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        [Display(Name = "Kullanıcı Adı")]

        public string? KullaniciAdi { get; set; }
        [Display(Name = "Doktor Adı")]

        public string? DoktorAdi { get; set; }
        public int DoktorId { get; set; }
        public DateTime RandevuSaati { get; set; }
    }
}
