using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Models
{
    public class Kullanicilar
    {
        public int Id { get; set; }

        [Display(Name = "Ad Soyad")]
        public string? KullaniciAdSoyad { get; set; }
        [Display(Name = "Kullanıcı Adı")]
        public string? KullaniciAdi  { get; set; }
        [Display(Name = "Kullanıcı Şifre")]

        public string? KullaniciSifre { get; set; }
        [Display(Name = "Kullanıcı EMail")]

        public string? KullaniciEmail  { get; set; }

    }
}
