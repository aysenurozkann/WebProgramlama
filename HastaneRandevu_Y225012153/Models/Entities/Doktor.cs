using System.ComponentModel.DataAnnotations;

namespace HastaneRandevu_Y225012153.Models.Domain
{
    public class Doktor
    {
        [Key]
        public int Id { get; set; }
        public string AdSoyad { get; set; }
        public int PoliklinikId { get; set; }
    }
}
