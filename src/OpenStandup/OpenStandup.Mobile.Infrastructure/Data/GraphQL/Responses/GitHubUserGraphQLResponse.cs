using Newtonsoft.Json;
using OpenStandup.Mobile.Infrastructure.Data.GraphQL.Types;

namespace OpenStandup.Mobile.Infrastructure.Data.GraphQL.Responses
{
    public class GitHubUserGraphQLResponse : BaseGraphQLResponse
    {
        public GitHubUser User { get; }

        [JsonConstructor] // Request when public, parameter-less ctor was added below to satisfy generic constraint in Polly fallback policy
        public GitHubUserGraphQLResponse(GitHubUser user) => User = user;

        public GitHubUserGraphQLResponse()
        {
        }
    }
}
