using TraktApiSharp;

namespace TraktDl.Business.Remote.Trakt
{
    public class TraktApiClientSandbox : ITraktApiClient
    {
        public ApiMode GetMode => ApiMode.Sandbox;

        public TraktClient Client
        {
            get
            {
                var client = new TraktClient("acea21223185b8046d9b2c3d7741aee8a5e2c4fbbcab24399bd3cae9cad2573e", "192523ccae3c2972fa5f338bbebbd3ed07802c70724b3c2e76b6fdeb9e353c57");

                client.Configuration.UseSandboxEnvironment = true;

                return client;
            }
        }
    }
}
