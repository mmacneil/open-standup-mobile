using System;
using SQLite;

namespace OpenStandup.Mobile.Infrastructure.Data.Model
{
    [Table("configuration")]
    public class ConfigurationData
    {
        [PrimaryKey]
        public string Version { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}

