using Docker.AutoDl.Database.SqLite;
using Microsoft.EntityFrameworkCore;

namespace TraktDl.Business.Database.SqLite
{
    public class SqLiteContext : DbContext
    {
        public DbSet<BlackListShowSqLite> BlackListShow { get; set; }

        public DbSet<ShowSqLite> Show { get; set; }
        public DbSet<SeasonSqLite> Season { get; set; }
        public DbSet<EpisodeSqLite> Episode { get; set; }

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


            //modelBuilder.Entity<SeasonSqLite>().HasKey(t => new { t.Show_ID, t.SeasonNumber });
            //modelBuilder.Entity<EpisodeSqLite>().HasKey(t => new { /*t.Show_ID, t.SeasonNumber*/t.Season, t.EpisodeNumber });

            //modelBuilder.Entity<ShowSqLite>().Property(t => t.Id).ValueGeneratedNever();
            //modelBuilder.Entity<SeasonSqLite>().Property(t => t.SeasonNumber).ValueGeneratedNever();
            //modelBuilder.Entity<EpisodeSqLite>().Property(t => t.EpisodeNumber).ValueGeneratedNever();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=AutoDl.db");
        }
    }
}
