

namespace OpenStandup.Mobile.Infrastructure.Data.GraphQL.Types
{
    public class MutationResponse
    {
        public string ClientMutationId { get; }

        public MutationResponse(string clientMutationId)
        {
            ClientMutationId = clientMutationId;
        }
    }
}
