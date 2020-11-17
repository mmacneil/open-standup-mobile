

namespace OpenStandup.Mobile.Infrastructure.Data.GraphQL.Types
{
    public class Connection
    { 
        public long TotalCount { get; }
        public Connection(long totalCount) => TotalCount = totalCount;
    }
}
