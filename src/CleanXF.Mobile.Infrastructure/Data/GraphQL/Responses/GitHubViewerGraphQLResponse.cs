using CleanXF.Core.Domain.Entities;


namespace CleanXF.Mobile.Infrastructure.Data.GraphQL.Responses
{
    public class GitHubViewerGraphQLResponse
    {
        public GitHubViewer Viewer { get; }
        public GitHubViewerGraphQLResponse(GitHubViewer viewer) => Viewer = viewer;
    }
}
