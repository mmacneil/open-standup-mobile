using System;
using System.Text;

namespace CleanXF.Core.Domain.Entities
{
    public class Followers
    {
        public Followers(long totalCount) => TotalCount = totalCount;

        public long TotalCount { get; }
    }

    public class GitHubUser
    {
        public GitHubUser(string name, string company, DateTimeOffset createdAt, Followers followers) =>
            (Name, Company, CreatedAt, FollowersCount) = (name, company, createdAt, followers.TotalCount);

        public string Name { get; }

        public string Company { get; }

        public DateTimeOffset CreatedAt { get; }

        public long FollowersCount { get; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"{nameof(Name)}: {Name}");
            stringBuilder.AppendLine($"{nameof(Company)}: {Company}");
            stringBuilder.AppendLine($"{nameof(CreatedAt)}: {CreatedAt:d}");
            stringBuilder.Append($"{nameof(FollowersCount)}: {FollowersCount}");

            return stringBuilder.ToString();
        }
    }
}
