

namespace OpenStandup.Core.Domain.Values
{
    public class Repositories
    {
        public Repositories(long totalCount) => TotalCount = totalCount;
        public long TotalCount { get; }
    }
}
