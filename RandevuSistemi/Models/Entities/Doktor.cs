using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandevuSistemi.Models.Entities
{
    public class Doktor
    {
        [Key]
        public int Id { get; set; }
        public string AdSoyad { get; set; }
        public int PoliklinikId { get; set; }

    }
}
