using System;
using System.Collections.Generic;


namespace OpenStandup.Core.Domain.Entities
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
        public long FollowerCount { get; }
        public long RepositoryCount { get; }
        public long FollowingCount { get; }
        public long GistCount { get; }
        public double Latitude { get; }
        public double Longitude { get; }
        public IEnumerable<Repository> Repositories { get; }

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
            long followerCount,
            long repositoryCount,
            long followingCount,
            long gistCount,
            double latitude, double longitude, IEnumerable<Repository> repositories) => (Id, Login, Name, AvatarUrl, BioHtml, WebsiteUrl, Company, Location, DatabaseId, Email, CreatedAt, FollowerCount, RepositoryCount, FollowingCount, GistCount, Latitude, Longitude, Repositories) =
            (id, login, name, avatarUrl, bioHtml, websiteUrl, company, location, databaseId, email, createdAt, followerCount, repositoryCount, followingCount, gistCount, latitude, longitude, repositories);
    }
}
