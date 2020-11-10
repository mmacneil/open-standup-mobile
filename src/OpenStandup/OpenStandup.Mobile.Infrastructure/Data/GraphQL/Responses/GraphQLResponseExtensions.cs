using System.Linq;
using GraphQL;
using Vessel;

namespace OpenStandup.Mobile.Infrastructure.Data.GraphQL.Responses
{
    public static class GraphQLResponseExtensions
    {
        public static bool IsSuccess<T>(this GraphQLResponse<T> @this) where T : BaseGraphQLResponse
        {
            return @this.Data?.Exception == null && @this.Errors == null;
        }

        public static Dto<TOut> GenerateFailedResponse<T, TOut>(this GraphQLResponse<T> @this) where T : BaseGraphQLResponse
        {
            if (@this.Data?.Exception != null)
            {
                return Dto<TOut>.Failed(@this.Data.Exception.StatusCode, @this.Data.Exception);
            }

            return @this.Errors != null ? Dto<TOut>.Failed(@this.Errors.Select(e => e.Message).ToArray()) : null;
        }
    }
}
