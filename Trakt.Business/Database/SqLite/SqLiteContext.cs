using Docker.AutoDl.Database.SqLite;
using Microsoft.EntityFrameworkCore;

namespace TraktDl.Business.Database.SqLite
{
    public class SqLiteContext : DbContext
    {
        public DbSet<BlackListShowSqLite> BlackListShow { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlackListShowSqLite>()
                .HasIndex(b => new { b.TraktShowId, b.Season })
                .IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=AutoDl.db");
        }
    }
}
