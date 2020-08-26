using SQLite;

namespace CleanXF.Mobile.Infrastructure.Data.Model
{
    [Table("session")]
    public class Session
    {
        public string AccessToken { get; set; }
    }
}


