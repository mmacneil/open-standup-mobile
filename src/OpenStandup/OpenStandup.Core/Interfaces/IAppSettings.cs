using System.Threading.Tasks;

namespace OpenStandup.Core.Interfaces
{
    public interface IAppSettings
    {
        string ApiEndpoint { get; }
        Task<string> GetGitHubClientId();
        Task<string> GetGitHubClientSecret();
    }
}
