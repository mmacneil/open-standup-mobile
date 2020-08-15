using CleanXF.Core.Domain.Entities;


namespace CleanXF.Mobile.Infrastructure.Data.GraphQL
{
    public class GitHubUserGraphQLResponse
    {
        public GitHubUser User { get; }
        public GitHubUserGraphQLResponse(GitHubUser user) => User = user;
    }
}
