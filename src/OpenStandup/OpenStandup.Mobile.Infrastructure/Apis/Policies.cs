using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using OpenStandup.Mobile.Infrastructure.Data.GraphQL.Responses;
using Polly;


namespace OpenStandup.Mobile.Infrastructure.Apis
{
    public static class Policies
    {
        private const int NumberOfTimesToRetry = 3, DelayMs = 100;

        public static async Task<HttpResponseMessage> AttemptAndRetryPolicy(Func<Task<HttpResponseMessage>> action)
        {
            var retryPolicy = Policy
                .Handle<Exception>()
                .RetryAsync(NumberOfTimesToRetry, async (exception, retryCount) =>
                {
                    await Task.Delay(DelayMs).ConfigureAwait(false);
                });

            var fallbackPolicy = Policy<HttpResponseMessage>
                .Handle<Exception>()
                .FallbackAsync((result, context, arg3) => Task.FromResult(result.Result ?? new HttpResponseMessage(HttpStatusCode.RequestTimeout) { ReasonPhrase = result.Exception.Message }), (result, context) => Task.CompletedTask);

            return await fallbackPolicy
                .WrapAsync(retryPolicy)
                .ExecuteAsync(async () => await action().ConfigureAwait(false))
               .ConfigureAwait(false);
        }

        public static async Task<GraphQLResponse<T>> AttemptAndRetryPolicy<T>(Func<Task<GraphQLResponse<T>>> action) where T : BaseGraphQLResponse, new()
        {
            var retryPolicy = Policy
                .Handle<Exception>()
                .RetryAsync(NumberOfTimesToRetry, async (exception, retryCount) =>
                {
                    await Task.Delay(DelayMs).ConfigureAwait(false);
                });

            var fallbackPolicy = Policy<GraphQLResponse<T>>
                .Handle<Exception>()
                .FallbackAsync((result, context, arg3) =>
                {
                    var exception = result.Exception switch
                    {
                        GraphQLHttpRequestException gqlRequestException => gqlRequestException,
                        HttpRequestException httpRequestException => new GraphQLHttpRequestException(
                            HttpStatusCode.RequestTimeout, null, httpRequestException.Message),
                        _ => null
                    };

                    return Task.FromResult(new GraphQLResponse<T>
                    {
                        Data = new T { Exception = exception }
                    });
                }, (result, context) => Task.CompletedTask);

            return await fallbackPolicy
                .WrapAsync(retryPolicy)
                .ExecuteAsync(async () => await action().ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}

