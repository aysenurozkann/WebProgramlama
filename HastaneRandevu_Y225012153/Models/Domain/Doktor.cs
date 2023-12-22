using System.ComponentModel.DataAnnotations;

namespace HastaneRandevu_Y225012153.Models.Domain
{
    public class Doktor
    {
        [Key]
        public int? DoktorID { get; set; }
        public string? DoktorAdi { get; set; }
        public string? DoktorSoyad { get; set; }
        public int? AnabilimDaliID { get; set; }
        public AnabilimDali? AnabilimDali { get; set; }
    }
}
