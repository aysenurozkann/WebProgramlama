namespace HastaneRandevuSistemi.Models
{
    public class Randevu
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public string? KullaniciAdi { get; set; }
        public string? DoktorAdi { get; set; }
        public int DoktorId { get; set; }
        public DateTime RandevuSaati { get; set; }
    }
}
