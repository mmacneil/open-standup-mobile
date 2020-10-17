using System.Threading.Tasks;
using CleanXF.Core.Domain.Entities;
using CleanXF.Core.Dto.Api;
using CleanXF.SharedKernel;

namespace CleanXF.Core.Interfaces.Apis
{
    public interface IOpenStandupApi
    {
        Task<HttpOperationResponse<string>> SaveProfile(GitHubUser gitHubUser);
        Task<HttpOperationResponse<AppConfigDto>> GetConfiguration();
        Task<HttpOperationResponse<string>> ValidateGitHubAccessToken(string token);
    }
}
