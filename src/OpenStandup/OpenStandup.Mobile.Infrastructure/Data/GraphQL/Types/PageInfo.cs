
namespace OpenStandup.Mobile.Infrastructure.Data.GraphQL.Types
{
    public class PageInfo
    {
        public string EndCursor { get; }
        public bool HasNextPage { get; }

        public PageInfo(string endCursor, bool hasNextPage)
        {
            EndCursor = endCursor;
            HasNextPage = hasNextPage;
        }
    }
}

 