namespace HastaneRandevu_Y225012153.Models.Domain
{
    public class AnabilimDali
    {
        public int? AnabilimDaliID { get; set; }
        public string? AnabilimDaliAdi { get; set; }
        public ICollection<Doktor>? Doktorlar { get; set; }
    }
}
