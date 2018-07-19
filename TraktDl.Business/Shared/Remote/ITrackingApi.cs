using System.Collections.Generic;

namespace TraktDl.Business.Shared.Remote
{
    public interface ITrackingApi
    {
        string GetMode { get; }

        /// <summary>
        /// Get the missing episodes
        /// </summary>
        /// <returns></returns>
        List<Show> GetMissingEpisodes();
    }
}
