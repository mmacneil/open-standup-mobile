

namespace CleanXF.Core.Domain.Entities
{
    public class GitHubViewer
    {
        public GitHubViewer(string login, string name) => (Login, Name) = (login, name);

        public string Login { get; }
        public string Name { get; }
    }
}
