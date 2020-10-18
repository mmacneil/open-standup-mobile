using OpenStandup.Core.Domain.Entities;
using System.Threading.Tasks;

namespace OpenStandup.Core.Interfaces.Data.GraphQL
{
    public interface IGitHubGraphQLApi
    {        
        Task<GitHubUser> GetGitHubViewer();
    }
}
