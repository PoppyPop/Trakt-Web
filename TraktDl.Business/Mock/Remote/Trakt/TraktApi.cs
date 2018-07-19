using System.Collections.Generic;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Business.Mock.Remote.Trakt
{
    public class TraktApi : ITrackingApi
    {
        public string GetMode => "Mock";

        public List<Show> GetMissingEpisodes()
        {
            List<Show> result = new List<Show>();

            Season season = new Season
            {
                SeasonNumber = 2,
                MissingEpisodes = new List<Episode> { new Episode { EpisodeNumber = 3 }, new Episode { EpisodeNumber = 4 }, new Episode { EpisodeNumber = 5 } }
            };

            result.Add(new Show
            {
                Id = 99718,
                SerieName = "Westworld",
                Providers = new Dictionary<string, string> { { "Imdb", "tt0475784" }, { "Tmdb", "63247" } },
                Seasons = new List<Season> { season }
            });

            result.Add(new Show
            {
                Id = 0,
                SerieName = "z fake",
            });

            result.Add(new Show
            {
                Id = 0,
                SerieName = "9 fake",
            });

            return result;
        }
    }
}
