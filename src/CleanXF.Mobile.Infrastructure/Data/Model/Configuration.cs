using System;
using SQLite;

namespace CleanXF.Mobile.Infrastructure.Data.Model
{
    [Table("configuration")]
    public class Configuration
    {
        public DateTimeOffset Created { get; set; }
        public string Version { get; set; }
    }
}

