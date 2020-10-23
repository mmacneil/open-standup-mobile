using System;
using System.Net.Http;
using System.Threading;
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
                 .FallbackAsync(HttpFallbackAction, OnHttpFallbackAsync);

             return await fallbackPolicy
                 .WrapAsync(retryPolicy)
                 .ExecuteAsync(async () => await action().ConfigureAwait(false))
                 .ConfigureAwait(false);
         }

         private static Task OnHttpFallbackAsync(DelegateResult<HttpResponseMessage> response, Context context)
         {
             return Task.CompletedTask;
         }

         private static Task<HttpResponseMessage> HttpFallbackAction(DelegateResult<HttpResponseMessage> responseToFailedRequest, Context context, CancellationToken cancellationToken)
         {
            var httpResponseMessage = new HttpResponseMessage(responseToFailedRequest.Result.StatusCode)
            {
                Content = new StringContent($"The fallback executed, the original error was {responseToFailedRequest.Result.ReasonPhrase}")
            };
            return Task.FromResult(httpResponseMessage);
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
                .FallbackAsync(FallbackAction, OnFallbackAsync);

            return await fallbackPolicy
                .WrapAsync(retryPolicy)
                .ExecuteAsync(async () => await action().ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        private static Task<GraphQLResponse<T>> FallbackAction<T>(DelegateResult<GraphQLResponse<T>> responseToFailedRequest, Context context, CancellationToken cancellationToken) where T : BaseGraphQLResponse, new()
        {
            return Task.FromResult(new GraphQLResponse<T>
            {
                Data = new T { Exception = (GraphQLHttpRequestException)responseToFailedRequest.Exception }
            });
        }

        private static Task OnFallbackAsync<T>(DelegateResult<GraphQLResponse<T>> response, Context context) where T : BaseGraphQLResponse
        {
            return Task.CompletedTask;
        }
    }
}

