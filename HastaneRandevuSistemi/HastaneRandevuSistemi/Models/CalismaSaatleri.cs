namespace HastaneRandevuSistemi.Models
{
    public class CalismaSaatleri
    {
        public int Id { get; set; }
        public int DoktorId { get; set; }
        public string? DoktorAdi { get; set; }
        public DateTime CalismaSaati { get; set; }
    }
}
