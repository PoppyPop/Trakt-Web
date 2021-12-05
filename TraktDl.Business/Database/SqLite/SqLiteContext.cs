//using System.Runtime.InteropServices;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Logging.Debug;
//using TraktDl.Business.Shared.Database;

//namespace TraktDl.Business.Database.SqLite
//{
//    public class SqLiteContext : DbContext
//    {
//        public DbSet<ApiKeySql> ApiKeys { get; set; }

//        public DbSet<ShowSql> Shows { get; set; }

//        public DbSet<SeasonSql> Seasons { get; set; }

//        public DbSet<EpisodeSql> Episodes { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<ShowSql>()
//                .HasKey(s => s.Id);
//            modelBuilder.Entity<ShowSql>()
//                .Property(s => s.Id).ValueGeneratedNever();

//            modelBuilder.Entity<ShowSql>()
//                .Ignore(s => s.Providers)
//                .Property(s => s.Blacklisted).IsRequired();


//            modelBuilder.Entity<SeasonSql>()
//                .HasKey(s => s.Id);
//            modelBuilder.Entity<SeasonSql>()
//                .Property(s => s.Id).ValueGeneratedOnAdd();

//            modelBuilder.Entity<SeasonSql>()
//                .Property(s => s.Blacklisted).IsRequired();

//            modelBuilder.Entity<SeasonSql>()
//                .HasIndex(s => new { s.ShowID, s.SeasonNumber })
//                .IsUnique();

//            modelBuilder.Entity<SeasonSql>()
//                .HasOne(c => c.Show)
//                .WithMany(p => p.Seasons)
//                .HasForeignKey(c => c.ShowID);


//            modelBuilder.Entity<EpisodeSql>()
//                .HasKey(e => e.Id);
//            modelBuilder.Entity<EpisodeSql>()
//                .Property(e => e.Id).ValueGeneratedOnAdd();

//            modelBuilder.Entity<EpisodeSql>()
//                .Ignore(e => e.Providers)
//                .Property(e => e.Status).IsRequired();

//            modelBuilder.Entity<EpisodeSql>()
//                .HasIndex(e => new { e.SeasonID, e.EpisodeNumber })
//                .IsUnique();

//            modelBuilder.Entity<EpisodeSql>()
//                .HasOne(e => e.Season)
//                .WithMany(s => s.Episodes)
//                .HasForeignKey(e => e.SeasonID);


//            modelBuilder.Entity<ApiKeySql>()
//                .HasKey(s => s.Id);
//            modelBuilder.Entity<ApiKeySql>()
//                .Property(s => s.Id).ValueGeneratedNever();
//            modelBuilder.Entity<ApiKeySql>()
//                .Property(s => s.ApiData).IsRequired();

//        }

//        public static readonly LoggerFactory MyLoggerFactory
//            = new LoggerFactory(new[]
//            {
//                new DebugLoggerProvider((category, level)
//                    => category == DbLoggerCategory.Database.Command.Name
//                       && level == LogLevel.Information)
//            });

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            bool isWindows = System.Runtime.InteropServices.RuntimeInformation
//                .IsOSPlatform(OSPlatform.Windows);

//            bool isLinux = System.Runtime.InteropServices.RuntimeInformation
//                .IsOSPlatform(OSPlatform.Linux);

//            string dataSource = "";

//            if (isWindows)
//            {
//                dataSource = "Data Source=AutoDl.db";
//            }
//            else if (isLinux)
//            {
//                dataSource = "Data Source=/datas/AutoDl.db";
//            }

//            optionsBuilder
//                .UseSqlite(dataSource)
//                .UseLazyLoadingProxies()
//                //.EnableSensitiveDataLogging()
//                //.UseLoggerFactory(MyLoggerFactory)
//                ;
//        }
//    }
//}
