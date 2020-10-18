
namespace OpenStandup.Core.Domain.Values
{
    public class Following
    {
        public Following(long totalCount) => TotalCount = totalCount;
        public long TotalCount { get; }
    }
}
