using TraktApiSharp;

namespace TraktDl.Business.Remote.Trakt
{
    public interface ITraktApiClient
    {
        string GetMode { get; }

        TraktClient Client { get; }
    }
}
