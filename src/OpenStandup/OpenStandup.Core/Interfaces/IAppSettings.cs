using System.Threading.Tasks;

namespace OpenStandup.Core.Interfaces
{
    public interface IAppSettings
    {
        string Host { get; }
        string ApiEndpoint { get; }
        Task<string> GetGitHubClientId();
        Task<string> GetGitHubClientSecret();
    }
}
