using Microsoft.EntityFrameworkCore;
using RandevuSistemi.Models.Entities;

namespace RandevuSistemi.Models
{
    public class Context : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-HN4124L\\SQLEXPRESS;database=HastaneDb;Trusted_connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true;");
        }

        public DbSet<AnaBilimDali> AnaBilimDallari { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Doktor> Doktorlar { get; set; }
        public DbSet<Poliklinik> Poliklinikler { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<Hizmetler> Hizmetler { get; set; }
        public DbSet<CalismaSaatleri> CalismaSaatleri { get; set; }
    }
}
