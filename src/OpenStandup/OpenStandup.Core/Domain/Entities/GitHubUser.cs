using OpenStandup.Core.Domain.Values;
using System;

namespace OpenStandup.Core.Domain.Entities
{
    public class GitHubUser
    {
        public string Id { get; }
        public string Name { get; }
        public string Login { get; }
        public string AvatarUrl { get; }
        public string BioHTML { get; }
        public string WebsiteUrl { get; }
        public string Company { get; }
        public string Location { get; }
        public long DatabaseId { get; }
        public string Email { get; }
        public DateTimeOffset CreatedAt { get; }
        public Followers Followers { get; }
        public Following Following { get; }
        public Repositories Repositories { get; }
        public Gists Gists { get; }

        public GitHubUser(
            string id,
            string login,
            string name,
            string avatarUrl,
            string bioHTML,
            string websiteUrl,
            string company,
            string location,
            long databaseId,
            string email,
            DateTimeOffset createdAt,
            Followers followers,
            Following following,
            Repositories repositories,
            Gists gists) => (Id, Login, Name, AvatarUrl, BioHTML, WebsiteUrl, Company, Location, DatabaseId, Email, CreatedAt, Followers, Following, Repositories, Gists) =
            (id, login, name, avatarUrl, bioHTML, websiteUrl, company, location, databaseId, email, createdAt, followers, following, repositories, gists);
    }
}
