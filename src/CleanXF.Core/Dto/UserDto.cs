using System;


namespace CleanXF.Core.Dto
{
    public class UserDto
    {
        public string Name { get; }
        public string Login { get; }
        public string AvatarUrl { get; }
        public string BioHTML { get; }
        public string WebsiteUrl { get; }
        public string Company { get; }
        public string Email { get; }
        public DateTimeOffset CreatedAt { get; }
        public string Location { get; }
        public long FollowerCount { get; }
        public long FollowingCount { get; }
        public long RepositoryCount { get; }
        public long GistCount { get; }
        public long DatabaseId { get; }
        public string GitHubId { get; }

        public UserDto(
            string name,
            string login,
            string avatarUrl,
            string bioHTML,
            string websiteUrl,
            string company, string email,
            DateTimeOffset createdAt, string location, long followerCount, long followingCount, long repositoryCount,
            long gistCount, long databaseId, string gitHubId)
        {
            Name = name;
            Login = login;
            AvatarUrl = avatarUrl;
            BioHTML = bioHTML;
            WebsiteUrl = websiteUrl;
            Company = company;
            Email = email;
            CreatedAt = createdAt;
            Location = location;
            FollowerCount = followerCount;
            FollowingCount = followingCount;
            RepositoryCount = repositoryCount;
            GistCount = gistCount;
            DatabaseId = databaseId;
            GitHubId = gitHubId;
        }
    }
}
