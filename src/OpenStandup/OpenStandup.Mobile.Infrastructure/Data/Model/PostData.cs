using System;
using SQLite;

namespace OpenStandup.Mobile.Infrastructure.Data.Model
{
    [Table("posts")]
    public class PostData
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
        public int StatusId { get; set; }
        public string Text { get; set; }
        public byte?[] Image { get; set; }
    }
}
