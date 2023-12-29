using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Models
{
    public class Poliklinikler
    {
        public int Id { get; set; }
        [Display(Name ="Poliklinik Adı")]
        public string? PoliklinikAdi { get; set; }
        [Display(Name ="Anabilim Dalı")]
        public int AnaBilimDaliId { get; set; }
    }
}
