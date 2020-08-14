using CleanXF.Core.Domain.Entities;
using System.Threading.Tasks;

namespace CleanXF.Core.Interfaces.Data.GraphQL
{
    public interface IGitHubGraphQLApi
    {
        Task<GitHubUser> GetGitHubUser(string login);
    }
}
