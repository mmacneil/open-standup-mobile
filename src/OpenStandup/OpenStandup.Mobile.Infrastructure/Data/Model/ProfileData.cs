using SQLite;
using System;


namespace OpenStandup.Mobile.Infrastructure.Data.Model
{
    [Table("profile")]
    public class ProfileData
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string AvatarUrl { get; set; }
        public string BioHtml { get; set; }
        public string WebsiteUrl { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Location { get; set; }
        public long DatabaseId { get; set; }
        public long FollowerCount { get; set; }
        public long FollowingCount { get; set; }
        public long RepositoryCount { get; set; }
        public long GistCount { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
