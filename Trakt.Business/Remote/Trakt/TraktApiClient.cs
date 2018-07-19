using TraktApiSharp;

namespace TraktDl.Business.Remote.Trakt
{
    public class TraktApiClient : ITraktApiClient
    {
        public string GetMode => "Production";

        public TraktClient Client
        {
            get
            {
                var client = new TraktClient("69e69b2e6312237ffea4f0b8c4c5493807743ec84b6c8cee57adc8ef98f0129e",
                    "2c2e46f552f496b68c8003c6f05795c00c42d77fe90d42907055352af3f737d0");

                return client;
            }
        }
    }
}
