using System.Threading.Tasks;

namespace CleanXF.Core.Interfaces.Data.Repositories
{
    public interface ISecureDataRepository
    {
        Task<string> GetGitHubClientId();
        Task<string> GetGitHubClientSecret();
        Task<string> GetPersonalAccessToken();
        Task SetGitHubClientId(string value);
        Task SetGitHubClientSecret(string value);
        Task SetPersonalAccessToken(string value);
        bool RemovePersonalAccessToken();
        Task<bool> HasAccessToken();
    }
}
