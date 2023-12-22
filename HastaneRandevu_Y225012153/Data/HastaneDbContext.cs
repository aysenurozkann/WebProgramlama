using HastaneRandevu_Y225012153.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace HastaneRandevu_Y225012153.Data
{
    public class HastaneDbContext : DbContext
    {
        public HastaneDbContext()
        {
        }

        public HastaneDbContext(DbContextOptions<HastaneDbContext> options) : base(options) 
        { 
        
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        optionsBuilder.UseSqlServer("server=DESKTOP-HN4124L\\SQLEXPRESS;database=DoktorDb;Trusted_connection=true");
         
        }


        public DbSet<Doktor>? Doktorlar { get; set; }
        public DbSet<AnabilimDali>? AnabilimDallari { get; set; }
        public DbSet<CalismaSaati>? CalismaSaatleri { get; set; }
        public DbSet<Randevu>? Randevular {  get; set; }
    }
}
