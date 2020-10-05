using System.Threading.Tasks;
using CleanXF.Core.Interfaces;
using CleanXF.Mobile.Infrastructure.Interfaces;


namespace CleanXF.Mobile.Infrastructure.Configuration
{
    public class AppSettings : IAppSettings
    {
        private readonly ISecureDataRepository _secureDataRepository;

        public AppSettings(ISecureDataRepository secureDataRepository)
        {
            _secureDataRepository = secureDataRepository;
        }

        public string ApiEndpoint
        {
            get
            {
#if DEBUG
                return "http://10.0.2.2:5000";
#else
                return "https://openstandup.com/api;
#endif
            }
        }

        public async Task<string> GetGitHubClientId()
        {
            return await _secureDataRepository.GetGitHubClientId();
        }

        public async Task<string> GetGitHubClientSecret()
        {
            return await _secureDataRepository.GetGitHubClientSecret();
        }
    }
}

 