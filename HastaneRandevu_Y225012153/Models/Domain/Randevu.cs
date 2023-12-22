namespace HastaneRandevu_Y225012153.Models.Domain
{
    public class Randevu
    {
        public int? RandevuID { get; set; }
        public int? DoktorID { get; set; }
        public string? HastaAdi { get; set;}
        public string? HastaSoyadi { get; set; }
        public DateTime? RandevuTarih { get; set; }

        public Doktor? Doktor { get; set; }
    }
}
