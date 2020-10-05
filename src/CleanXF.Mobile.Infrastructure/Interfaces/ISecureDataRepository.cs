using System.Threading.Tasks;

namespace CleanXF.Mobile.Infrastructure.Interfaces
{
    public interface ISecureDataRepository
    {
        Task<string> GetGitHubClientId();
        Task<string> GetGitHubClientSecret();
        Task SetGitHubClientId(string value);
        Task SetGitHubClientSecret(string value);
    }
}
