using OpenStandup.Core.Domain.Entities;
using System.Threading.Tasks;
using Vessel;

namespace OpenStandup.Core.Interfaces.Data.GraphQL
{
    public interface IGitHubGraphQLApi
    {
        Task<Dto<bool>> Follow(string userId);
        Task<Dto<bool>> Unfollow(string userId);
        Task<Dto<GitHubUser>> GetFollowerStatus(string login);
        Task<Dto<GitHubUser>> GetViewer();
    }
}
