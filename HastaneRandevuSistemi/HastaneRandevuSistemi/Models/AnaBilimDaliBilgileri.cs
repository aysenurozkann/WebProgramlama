using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Models
{
    public class AnaBilimDaliBilgileri
    {
        public Poliklinikler? poliklinikler { get; set; }
        [Display(Name ="Anabilim Dalı Adı")]
        public string? AnaBilimDallariAdi { get; set; }
    }
}
