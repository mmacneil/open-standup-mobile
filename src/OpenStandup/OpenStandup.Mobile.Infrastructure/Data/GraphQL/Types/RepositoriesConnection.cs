using System.Collections.Generic;

namespace OpenStandup.Mobile.Infrastructure.Data.GraphQL.Types
{
    public class RepositoriesConnection : Connection
    {
        public ICollection<Repository> Nodes { get; }
        public PageInfo PageInfo { get; }

        public RepositoriesConnection(ICollection<Repository> nodes, PageInfo pageInfo, long totalCount) : base(totalCount)
        {
            Nodes = nodes;
            PageInfo = pageInfo;
        }
    }
}
