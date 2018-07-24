using TraktApiSharp;

namespace TraktDl.Business.Remote.Trakt
{
    public interface ITraktApiClient
    {
        ApiMode GetMode { get; }

        TraktClient Client { get; }
    }

    public enum ApiMode
    {
        Production,
        Sandbox
    }
}
