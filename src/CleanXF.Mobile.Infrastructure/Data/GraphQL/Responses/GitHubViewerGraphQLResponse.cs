using CleanXF.Core.Domain.Entities;


namespace CleanXF.Mobile.Infrastructure.Data.GraphQL.Responses
{
    public class GitHubViewerGraphQLResponse
    {
        public GitHubUser Viewer { get; }
        public GitHubViewerGraphQLResponse(GitHubUser viewer) => Viewer = viewer;
    }
}
