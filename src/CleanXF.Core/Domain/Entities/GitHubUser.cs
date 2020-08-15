using System;


namespace CleanXF.Core.Domain.Entities
{
    public class Followers
    {
        public Followers(long totalCount) => TotalCount = totalCount;

        public long TotalCount { get; }
    }

    public class GitHubUser
    {
        public GitHubUser(string name, string avatarUrl, string company, DateTimeOffset createdAt, Followers followers) =>
            (Name, AvatarUrl, Company, CreatedAt, FollowersCount) = (name, avatarUrl, company, createdAt, followers.TotalCount);

        public string Name { get; }

        public string AvatarUrl { get; }

        public string Company { get; }

        public DateTimeOffset CreatedAt { get; }

        public long FollowersCount { get; }
    }
}
