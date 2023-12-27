namespace HastaneRandevuSistemi.Models
{
    public class PoliklinikBilgileri
    {
        public Doktor? doktor { get; set; }
        public AnaBilimDali? AnabilimDaliId { get; set; } // bu satırı ekledim
        public string? PolikliniklerinAdi { get; set; }
    }
}
