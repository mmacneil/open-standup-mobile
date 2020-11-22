using SQLite;


namespace OpenStandup.Mobile.Infrastructure.Data.Model
{
    [Table("repositories")]
    public class RepositoryData
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsPrivate { get; set; }
    }
}