using TMDbLib.Client;
using TMDbLib.Objects.TvShows;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Business.Remote.Tmdb
{
    public class Tmdb : IImageApi
    {
        public Tmdb()
        {
            TMDbClient client = new TMDbClient("8ba7498e29bcfba224bcf8161d734158");

            client.GetTvShowAsync(48866, TvShowMethods.Undefined, "fr-FR");

            // The 100 48866
            client.GetTvEpisodeAsync(48866, 5, 1, TvEpisodeMethods.Undefined, "fr-FR");
        }
    }
}
