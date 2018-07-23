using System;
using TraktDl.Business.Database.SqLite;
using TraktDl.Business.Shared.Database;
using System.Linq;

namespace TraktDl.Business.Shared.Remote
{
    public static class DbExtend
    {
        public static Shared.Remote.Episode Convert(this EpisodeSql ep)
        {
            Shared.Remote.Episode episode = new Shared.Remote.Episode(ep.Id)
            {
                EpisodeNumber = ep.EpisodeNumber,
                Providers = ep.Providers.ToDictionary(
                    pair => (Provider)Enum.Parse(typeof(Provider), Enum.GetName(typeof(ProviderSql), pair.Key)),
                    pair => pair.Value),
                PosterUrl = ep.PosterUrl,
                Status = (EpisodeStatus)Enum.Parse(typeof(EpisodeStatus),
                    Enum.GetName(typeof(EpisodeStatusSql), ep.Status)),
                SeasonNumber = ep.Season.SeasonNumber,
                AirDate = ep.AirDate?.ToString("dd/MM/yyyy"),
                Name = ep.Name,

            };

            return episode;
        }


        public static Shared.Remote.Season Convert(this SeasonSql sea)
        {
            Shared.Remote.Season season = new Shared.Remote.Season(sea.Id)
            {
                SeasonNumber = sea.SeasonNumber,
                Blacklisted = sea.Blacklisted,
                Episodes = sea.Episodes.Select(s => s.Convert()).ToList(),
            };

            return season;
        }

        public static Shared.Remote.Show Convert(this ShowSql sho)
        {
            Shared.Remote.Show show = new Shared.Remote.Show
            {
                Id = sho.Id,
                Blacklisted = sho.Blacklisted,
                SerieName = sho.Name,
                Year = sho.Year,
                Providers = sho.Providers.ToDictionary(
                    pair => (Provider)Enum.Parse(typeof(Provider), Enum.GetName(typeof(ProviderSql), pair.Key)),
                    pair => pair.Value),
                PosterUrl = sho.PosterUrl,
            };

            foreach (var seasonSqLite in sho.Seasons)
            {
                show.Seasons.Add(seasonSqLite.Convert());
            }

            return show;
        }
    }
}
