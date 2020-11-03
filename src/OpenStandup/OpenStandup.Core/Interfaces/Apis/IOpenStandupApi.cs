using System.Threading.Tasks;
using OpenStandup.Core.Domain.Entities;
using OpenStandup.Core.Dto.Api;
using OpenStandup.SharedKernel;

namespace OpenStandup.Core.Interfaces.Apis
{
    public interface IOpenStandupApi
    {
        Task<Result<bool>> SaveProfile(GitHubUser gitHubUser);
        Task<Result<AppConfigDto>> GetConfiguration();
        Task<Result<string>> ValidateGitHubAccessToken(string token);
    }
}
