using System.Threading.Tasks;
using OpenStandup.Core.Domain.Entities;
using OpenStandup.Core.Dto.Api;
using OpenStandup.SharedKernel;

namespace OpenStandup.Core.Interfaces.Apis
{
    public interface IOpenStandupApi
    {
        Task<HttpOperationResponse<string>> SaveProfile(GitHubUser gitHubUser);
        Task<HttpOperationResponse<AppConfigDto>> GetConfiguration();
        Task<HttpOperationResponse<string>> ValidateGitHubAccessToken(string token);
    }
}
