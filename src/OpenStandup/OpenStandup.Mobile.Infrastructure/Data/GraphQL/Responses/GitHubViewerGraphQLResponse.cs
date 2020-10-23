using Newtonsoft.Json;
using OpenStandup.Core.Domain.Entities;


namespace OpenStandup.Mobile.Infrastructure.Data.GraphQL.Responses
{
    public class GitHubViewerGraphQLResponse : BaseGraphQLResponse
    {
        public GitHubUser Viewer { get; }

        [JsonConstructor] // Request when public, parameter-less ctor was added below to satisfy generic constraint in Polly fallback policy
        public GitHubViewerGraphQLResponse(GitHubUser viewer) => Viewer = viewer;

        public GitHubViewerGraphQLResponse()
        {
        }
    }
}
