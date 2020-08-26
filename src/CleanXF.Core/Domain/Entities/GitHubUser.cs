using CleanXF.Core.Domain.Values;
using System;

namespace CleanXF.Core.Domain.Entities
{
    public class GitHubUser
    {
        public string Login { get; }
        public string Name { get; }
        public string AvatarUrl { get; }
        public string BioHTML { get; }
        public string Company { get; }
        public string Location { get; }
        public string Email { get; }
        public DateTimeOffset CreatedAt { get; }
        public Followers Followers { get; }
        public Following Following { get; }

        public GitHubUser(
            string login,
            string name,
            string avatarUrl,
            string bioHTML,
            string company,
            string location,
            string email,
            DateTimeOffset createdAt,
            Followers followers,
            Following following) => (Login, Name, AvatarUrl, BioHTML, Company, Location, Email, CreatedAt, Followers, Following) =
            (login, name, avatarUrl, bioHTML, company, location, email, createdAt, followers, following);
    }
}
