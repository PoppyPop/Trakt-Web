using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using TraktDl.Business.Shared.Database;

namespace TraktDl.Business.Database.SqLite
{
    public class NHibernateDatabase : IDatabase
    {
        private ISessionFactory sessionFactory;
        private ISession session;
        private ITransaction transaction;

        public NHibernateDatabase()
        {
            sessionFactory = CreateSessionFactory();
        }
        public void OpenTransaction()
        {
            session = sessionFactory.OpenSession();
            transaction = session.BeginTransaction();
        }

        public void Commit()
        {
            session.Flush();
            if (transaction != null)
                transaction.Commit();
        }

        public void Rollback()
        {
            if (transaction != null)
                transaction.Rollback();
        }

        public void AddApiKey(ApiKeySql apiKey)
        {
            var exist = session.Query<ApiKeySql>().SingleOrDefault(b => b.Id == apiKey.Id);

            if (exist == null)
            {
                session.Save(apiKey);
            }
            else
            {
                exist.ApiData = apiKey.ApiData;
            }
        }

        public ApiKeySql GetApiKey(string name)
        {
            return session.Query<ApiKeySql>().SingleOrDefault(k => k.Id == name);
        }

        public void AddOrUpdateShows(List<ShowSql> shows)
        {
            foreach (var show in shows)
            {
                var bddShow = session.Query<ShowSql>().SingleOrDefault(b => b.Id == show.Id);

                if (bddShow == null)
                {
                    session.Save(show);
                }
            }
        }

        public List<ShowSql> GetShows()
        {
            return session.Query<ShowSql>().ToList();
        }

        public ShowSql GetShow(uint id)
        {
            return session.Query<ShowSql>().SingleOrDefault(s => s.Id == id);
        }

        public List<ShowSql> GetMissingEpisode()
        {
            return session.Query<ShowSql>()
                        .Where(show =>
                            show.Seasons.SelectMany(season => season.Episodes)
                                .Any((ep => ep.Status == EpisodeStatusSql.Missing)))
                        .Distinct()
                        .ToList();
        }

        public List<ShowSql> GetMissingImages()
        {
            return session.Query<ShowSql>()
                        .Where(show =>
                            show.Seasons.SelectMany(season => season.Episodes)
                                .Any((ep => ep.Status == EpisodeStatusSql.Missing))
                            && ((show.PosterUrl != null && show.PosterUrl != string.Empty) || show.Seasons.SelectMany(season => season.Episodes)
                                .Any(ep => (ep.PosterUrl != null && ep.PosterUrl != string.Empty)))
                            )
                        .Distinct()
                        .ToList();
        }

        public void ClearMissingEpisodes()
        {
            var episodes = session.Query<EpisodeSql>().Where(e => e.Status == EpisodeStatusSql.Missing);
            foreach (var episodeSqLite in episodes)
            {
                episodeSqLite.Status = EpisodeStatusSql.Unknown;
            }
        }

        public void ClearUnknownEpisodes()
        {
            var episodes = session.Query<EpisodeSql>().Where(e => e.Status == EpisodeStatusSql.Unknown);
            foreach (var episodeSqLite in episodes)
            {
                episodeSqLite.Status = EpisodeStatusSql.Collected;
            }
        }

        public bool ResetBlacklist()
        {
            var blacklistedShow = session.Query<ShowSql>().Where(s => s.Blacklisted);
            foreach (var showSqLite in blacklistedShow)
            {
                showSqLite.Blacklisted = false;
            }

            var blacklistedSeason = session.Query<SeasonSql>().Where(s => s.Blacklisted);
            foreach (var seasonSqLite in blacklistedSeason)
            {
                seasonSqLite.Blacklisted = false;
            }

            return true;
        }

        public bool ResetImages()
        {
            var imagesShow = session.Query<ShowSql>().Where(s => !s.Blacklisted && !string.IsNullOrEmpty(s.PosterUrl));
            foreach (var showSqLite in imagesShow)
            {
                showSqLite.PosterUrl = null;
            }

            var imagesEpisodes = session.Query<EpisodeSql>().Where(s => !s.Season.Blacklisted && !s.Season.Show.Blacklisted && !string.IsNullOrEmpty(s.PosterUrl));
            foreach (var epîsodeSqLite in imagesEpisodes)
            {
                epîsodeSqLite.PosterUrl = null;
            }

            return true;
        }

        private static string DbFile = "TrakDL.db";

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(
                    SQLiteConfiguration.Standard
                        .UsingFile(DbFile)
                        .ShowSql()
                )
                .Mappings(m =>
                    m.FluentMappings.AddFromAssemblyOf<TraktDl.Business.Database.SqLite.ApiKeyMap>()
                        .AddFromAssemblyOf<TraktDl.Business.Database.SqLite.ShowMap>()
                        .AddFromAssemblyOf<TraktDl.Business.Database.SqLite.SeasonMap>()
                        .AddFromAssemblyOf<TraktDl.Business.Database.SqLite.EpisodeSqlMap>()
                    )
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            if (!File.Exists(DbFile))
            {
                // this NHibernate tool takes a configuration (with mapping info in)
                // and exports a database schema from it
                new SchemaExport(config)
                    .Create(false, true);
            }
            else
            {
                new SchemaUpdate(config)
                    .Execute(false, true);
            }
        }
    }
}
