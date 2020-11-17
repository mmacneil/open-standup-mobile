

namespace OpenStandup.Mobile.Infrastructure.Data.GraphQL.Types
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
}