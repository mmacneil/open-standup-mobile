using OpenStandup.Core.Domain.Entities;
using System.Threading.Tasks;
using Vessel;

namespace OpenStandup.Core.Interfaces.Data.GraphQL
{
    public interface IGitHubGraphQLApi
    {
        Task<Dto<bool>> CanFollow(string login);
        Task<Dto<GitHubUser>> GetViewer();
    }
}
