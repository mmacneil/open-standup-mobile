using System;
using SQLite;

namespace CleanXF.Mobile.Infrastructure.Data.Model
{
    [Table("configuration")]
    public class Configuration
    {
        [PrimaryKey]
        public string Version { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}

