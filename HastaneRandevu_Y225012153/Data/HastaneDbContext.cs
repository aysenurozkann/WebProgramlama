using HastaneRandevu_Y225012153.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace HastaneRandevu_Y225012153.Data
{
    public class HastaneDbContext : DbContext
    {
        public HastaneDbContext(DbContextOptions<HastaneDbContext> options) : base(options) 
        { 
        
        }

        public DbSet<Doktor> Doktorlar { get; set; }
    }
}
