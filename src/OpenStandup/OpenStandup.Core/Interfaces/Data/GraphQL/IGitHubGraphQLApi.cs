using OpenStandup.Core.Domain.Entities;
using System.Threading.Tasks;
using OpenStandup.SharedKernel;

namespace OpenStandup.Core.Interfaces.Data.GraphQL
{
    public interface IGitHubGraphQLApi
    {
        Task<GraphQLOperationResponse<GitHubUser>> GetGitHubViewer();
    }
}
