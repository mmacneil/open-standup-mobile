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
        public bool ViewerCanFollow { get; }
        public bool ViewerIsFollowing { get; }

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
            double latitude, double longitude,
            IEnumerable<Repository> repositories,
            bool viewerCanFollow,
            bool viewerIsFollowing)
        {
            Id = id;
            Login = login;
            Name = name;
            AvatarUrl = avatarUrl;
            BioHtml = bioHtml;
            WebsiteUrl = websiteUrl;
            Company = company;
            Location = location;
            DatabaseId = databaseId;
            Email = email;
            CreatedAt = createdAt;
            FollowerCount = followerCount;
            RepositoryCount = repositoryCount;
            FollowingCount = followingCount;
            GistCount = gistCount;
            Latitude = latitude;
            Longitude = longitude;
            Repositories = repositories;
            ViewerCanFollow = viewerCanFollow;
            ViewerIsFollowing = viewerIsFollowing;
        }
    }
}
