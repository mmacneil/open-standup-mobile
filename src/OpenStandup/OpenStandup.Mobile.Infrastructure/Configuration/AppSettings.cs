using System.Threading.Tasks;
using OpenStandup.Core.Interfaces;
using OpenStandup.Core.Interfaces.Data.Repositories;


namespace OpenStandup.Mobile.Infrastructure.Configuration
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
                return "https://192.168.0.141:45455/api"; // "http://10.0.2.2:5000/api";
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

 