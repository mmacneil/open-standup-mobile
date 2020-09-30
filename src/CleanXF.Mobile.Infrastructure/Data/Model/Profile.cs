using SQLite;
using System;


namespace CleanXF.Mobile.Infrastructure.Data.Model
{
    [Table("profile")]
    public class Profile
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string AvatarUrl { get; set; }
        public string BioHTML { get; set; }
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
    }
}
