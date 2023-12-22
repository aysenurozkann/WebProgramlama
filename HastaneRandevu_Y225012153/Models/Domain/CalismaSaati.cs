namespace HastaneRandevu_Y225012153.Models.Domain
{
    public class CalismaSaati
    {
        public int? CalismaSaatiID { get; set; }
        public int? DoktorID { get; set; }
        public DateTime? CalismaGunu { get; set; }
        public Doktor? Doktor { get; set; }
    }
}
