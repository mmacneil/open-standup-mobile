using OpenStandup.Core.Domain.Entities;


namespace OpenStandup.Mobile.Infrastructure.Data.GraphQL.Responses
{
    public class GitHubViewerGraphQLResponse
    {
        public GitHubUser Viewer { get; }
        public GitHubViewerGraphQLResponse(GitHubUser viewer) => Viewer = viewer;
    }
}
