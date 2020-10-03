using CleanXF.Core.Interfaces;

namespace CleanXF.Mobile.Infrastructure.Configuration
{
    public class AppSettings : IAppSettings
    {
        public string ApiEndpoint
        {
            get
            {
#if DEBUG
                return "https://localhost:5001/api";
#else
                return "https://openstandup.com/api;
#endif
            }
        }

        public string GitHubClientId { get; }
    }
}
