

namespace CleanXF.Core.Domain.Values
{
    public class Followers
    {
        public Followers(long totalCount) => TotalCount = totalCount;
        public long TotalCount { get; }
    }
}
