using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Models
{
    public class Doktor
    {
        [Key]
        public int Id { get; set; }
        public string? DoktorAdSoyad  { get; set; }
        public int PoliklinikId { get; set; }

    }
}
