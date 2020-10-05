using System.Threading.Tasks;

namespace CleanXF.Core.Interfaces
{
    public interface IAppSettings
    {
        string ApiEndpoint { get; }
        Task<string> GetGitHubClientId();
        Task<string> GetGitHubClientSecret();
    }
}
