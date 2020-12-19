using Newtonsoft.Json;
using OpenStandup.Mobile.Infrastructure.Data.GraphQL.Types;

namespace OpenStandup.Mobile.Infrastructure.Data.GraphQL.Responses
{
    public class FollowUserGraphQLResponse : BaseGraphQLResponse
    {
        public MutationResponse FollowUser { get; }

        [JsonConstructor] // Request when public, parameter-less ctor was added below to satisfy generic constraint in Polly fallback policy
        public FollowUserGraphQLResponse(MutationResponse followUser) => FollowUser = followUser;

        public FollowUserGraphQLResponse()
        {
        }
    }
}
