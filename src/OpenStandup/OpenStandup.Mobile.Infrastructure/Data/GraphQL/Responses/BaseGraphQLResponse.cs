using GraphQL.Client.Http;


namespace OpenStandup.Mobile.Infrastructure.Data.GraphQL.Responses
{
    public class BaseGraphQLResponse
    {
        public GraphQLHttpRequestException Exception { get; set; }
    }
}



