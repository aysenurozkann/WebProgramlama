using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Models
{
    public class AnaBilimDali
    {
        public int Id { get; set; }

        [Display(Name = "Sistemde Kayıtlı Anabilim Dallar")]
        public string? AnaBilimDaliAdi { get; set; }
    }
}
