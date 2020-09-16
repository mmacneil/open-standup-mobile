

namespace CleanXF.Core.Domain.Values
{
    public class Gists
    {
        public Gists(long totalCount) => TotalCount = totalCount;
        public long TotalCount { get; }
    }
}
