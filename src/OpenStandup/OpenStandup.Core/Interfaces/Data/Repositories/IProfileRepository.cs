using OpenStandup.Core.Domain.Entities;
using System.Threading.Tasks;

namespace OpenStandup.Core.Interfaces.Data.Repositories
{
    public interface IProfileRepository
    {
        Task<bool> InsertOrReplace(GitHubUser user);
        Task<GitHubUser> Get();
    }
}