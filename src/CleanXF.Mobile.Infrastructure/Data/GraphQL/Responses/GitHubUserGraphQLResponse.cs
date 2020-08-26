using CleanXF.Core.Domain.Entities;


namespace CleanXF.Mobile.Infrastructure.Data.GraphQL.Responses
{
    public class GitHubUserGraphQLResponse
    {
        public GitHubUser User { get; }
        public GitHubUserGraphQLResponse(GitHubUser user) => User = user;
    }
}
