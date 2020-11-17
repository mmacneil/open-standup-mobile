

namespace OpenStandup.Core.Domain.Entities
{
    public class Repository
    {
        public int Id { get; }
        public string Name { get; }
        public string Url { get; }
        public bool IsPrivate { get; }

        public Repository(int id, string name, string url, bool isPrivate)
        {
            Id = id;
            Name = name;
            Url = url;
            IsPrivate = isPrivate;
        }
    }
}
