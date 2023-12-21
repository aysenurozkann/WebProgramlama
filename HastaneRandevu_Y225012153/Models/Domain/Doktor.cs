using System.ComponentModel.DataAnnotations;

namespace HastaneRandevu_Y225012153.Models.Domain
{
    public class Doktor
    {
        [Key]
        public int DoktorId { get; set; }
        public string DoktorAdi { get; set; }
        public string DoktorKlinik { get; set; }
        public DateTime DoktorTarih { get; set; }
        public int? DoktorBransId { get; set; }
    }
}
