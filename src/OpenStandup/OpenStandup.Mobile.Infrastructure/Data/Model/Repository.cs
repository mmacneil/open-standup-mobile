using SQLite;

namespace OpenStandup.Mobile.Infrastructure.Data.Model
{
    [Table("repository")]
    public class Repository
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsPrivate { get; set; }
    }
}


