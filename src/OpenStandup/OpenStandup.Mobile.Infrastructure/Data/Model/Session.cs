using SQLite;

namespace OpenStandup.Mobile.Infrastructure.Data.Model
{
    [Table("session")]
    public class Session
    {
        public string AccessToken { get; set; }
    }
}


