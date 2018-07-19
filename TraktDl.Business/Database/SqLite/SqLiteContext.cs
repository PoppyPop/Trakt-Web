using Microsoft.EntityFrameworkCore;

namespace TraktDl.Business.Database.SqLite
{
    public class SqLiteContext : DbContext
    {
        public DbSet<BlackListShowSqLite> BlackListShows { get; set; }

        public DbSet<ApiKeySqLite> ApiKeys { get; set; }

        public DbSet<ShowSqLite> Shows { get; set; }
        public DbSet<SeasonSqLite> Seasons { get; set; }
        public DbSet<EpisodeSqLite> Episodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlackListShowSqLite>()
                .HasIndex(b => new { b.TraktShowId, b.Season })
                .IsUnique();


            modelBuilder.Entity<SeasonSqLite>()
                .HasIndex(b => new { b.ShowID, b.SeasonNumber })
                .IsUnique();

            modelBuilder.Entity<EpisodeSqLite>()
                .HasIndex(b => new { b.SeasonID, b.EpisodeNumber })
                .IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=AutoDl.db");
        }
    }
}
