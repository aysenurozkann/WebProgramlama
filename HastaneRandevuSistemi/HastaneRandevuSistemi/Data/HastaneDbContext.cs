using HastaneRandevuSistemi.Models;
using Microsoft.EntityFrameworkCore;

namespace HastaneRandevuSistemi.Data
{
    public class HastaneDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=HastaneDb;Trusted_Connection=True;MultipleActiveResultSets=true;"); // parantez içine ; koydum, hata veriyorsa kontrol et
        }
        public DbSet<AnaBilimDali> AnaBilimDallari { get; set; }
        public DbSet<Kullanicilar> Kullanicilar { get; set; } // burası hata verecek mi kontrol et, aynı isimde
        public DbSet<Doktor> Doktorlar { get; set; }
        public DbSet<Poliklinikler> Poliklinikler { get; set; } // burası hata verecek mi kontrol et, aynı isimde
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<CalismaSaatleri> CalismaSaatleri { get; set; }


    }
}
