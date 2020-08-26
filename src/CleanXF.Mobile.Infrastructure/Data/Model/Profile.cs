using SQLite;
using System;


namespace CleanXF.Mobile.Infrastructure.Data.Model
{
    [Table("profile")]
    public class Profile
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string AvatarUrl { get; set; }
        public string BioHTML { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Location { get; set; }
        public long FollowerCount { get; set; }
        public long FollowingCount { get; set; }
    }
}
