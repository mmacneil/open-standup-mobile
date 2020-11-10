

using System.Collections.Generic;

namespace OpenStandup.Core.Domain.Values
{
    public class Repository
    {
        public int DatabaseId { get; }
        public string Name { get; }
        public string Url { get; }
        public bool IsPrivate { get; }

        public Repository(int databaseId, string name, string url, bool isPrivate)
        {
            DatabaseId = databaseId;
            Name = name;
            Url = url;
            IsPrivate = isPrivate;
        }
    }
    public class Repositories
    {
        public Repositories(long totalCount, ICollection<Repository> nodes) => (TotalCount, Nodes) = (totalCount, nodes);
        public long TotalCount { get; }
        public ICollection<Repository> Nodes { get; }
    }
}
