using System;

namespace OpenStandup.Mobile.Infrastructure.Data.GraphQL.Types
{
    public class GitHubUser
    {
        public string Id { get; }
        public string Name { get; }
        public string Login { get; }
        public string AvatarUrl { get; }
        public string BioHtml { get; }
        public string WebsiteUrl { get; }
        public string Company { get; }
        public string Location { get; }
        public long DatabaseId { get; }
        public string Email { get; }
        public DateTimeOffset CreatedAt { get; }
        public Connection Followers { get; }
        public Connection Following { get; }
        public RepositoriesConnection Repositories { get; set; }
        public Connection Gists { get; }
        public double Latitude { get; }
        public double Longitude { get; }
        public bool ViewerCanFollow { get; }

        public GitHubUser(
            string id,
            string login,
            string name,
            string avatarUrl,
            string bioHtml,
            string websiteUrl,
            string company,
            string location,
            long databaseId,
            string email,
            DateTimeOffset createdAt,
            Connection followers,
            Connection following,
            RepositoriesConnection repositories,
            Connection gists, double latitude, double longitude, bool viewerCanFollow) =>
            (Id, Login, Name, AvatarUrl, BioHtml, WebsiteUrl, Company, Location, DatabaseId, Email, CreatedAt, Followers, Following, Repositories, Gists, Latitude, Longitude, ViewerCanFollow) =
            (id, login, name, avatarUrl, bioHtml, websiteUrl, company, location, databaseId, email, createdAt, followers, following, repositories, gists, latitude, longitude, viewerCanFollow);
    }
}
