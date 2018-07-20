using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;

namespace TraktDl.Business.Database.SqLite
{
    public class SqLiteContext : DbContext
    {
        public DbSet<ApiKeySqLite> ApiKeys { get; set; }

        public DbSet<ShowSqLite> Shows { get; set; }

        public DbSet<SeasonSqLite> Seasons { get; set; }

        public DbSet<EpisodeSqLite> Episodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<SeasonSqLite>()
                .HasIndex(b => new { b.ShowID, b.SeasonNumber })
                .IsUnique();

            modelBuilder.Entity<EpisodeSqLite>()
                .HasIndex(b => new { b.SeasonID, b.EpisodeNumber })
                .IsUnique();


            modelBuilder.Entity<EpisodeSqLite>()
            .HasOne(p => p.Season)
            .WithMany(b => b.Episodes)
            .HasForeignKey(p => p.SeasonID);

            modelBuilder.Entity<SeasonSqLite>()
                .HasOne(p => p.Show)
                .WithMany(b => b.Seasons)
                .HasForeignKey(p => p.ShowID);

        }

        public static readonly LoggerFactory MyLoggerFactory
            = new LoggerFactory(new[]
            {
                new DebugLoggerProvider((category, level)
                    => category == DbLoggerCategory.Database.Command.Name
                       && level == LogLevel.Information)
            });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite("Data Source=AutoDl.db")
                .UseLazyLoadingProxies()
                .EnableSensitiveDataLogging()
                .UseLoggerFactory(MyLoggerFactory);
        }
    }
}
