using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;

namespace CleanXF.Mobile.Infrastructure.Apis
{
    public static class Policies
    {
        private const int NumberOfTimesToRetry = 3;
        private const int RetryMultiple = 2;

        public static async Task<HttpResponseMessage> AttemptAndRetryPolicy(Func<Task<HttpResponseMessage>> action)
        {
            //Handle HttpRequestException when it occurs
            var response = await Policy.Handle<HttpRequestException>(ex =>
                {
                    Debug.WriteLine("Request failure.");
                    return true;
                })

                //wait for a given number of seconds which increases after each retry
                .WaitAndRetryAsync(NumberOfTimesToRetry, retryCount => TimeSpan.FromSeconds(retryCount * RetryMultiple))

                //After the retry, Execute the appropriate set of instructions
                .ExecuteAsync(async () => await action());

            //Return the response message gotten from the http client call.
            return response;
        }
    }
}
