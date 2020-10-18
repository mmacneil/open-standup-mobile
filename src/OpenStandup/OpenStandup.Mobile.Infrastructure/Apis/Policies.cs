using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
                .FallbackAsync(fallbackValue: new HttpResponseMessage(HttpStatusCode.RequestTimeout));

            return await fallbackPolicy
                .WrapAsync(retryPolicy)
                .ExecuteAsync(async () => await action().ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}

