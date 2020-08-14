using CleanXF.Core.Interfaces.Authentication;
using CleanXF.SharedKernel;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CleanXF.Mobile.Infrastructure.Authentication.GitHub
{
    public class OAuthAuthenticator : IAuthenticator
    {
        private readonly HttpClient _httpClient;

        public OAuthAuthenticator(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OperationResponse<string>> Authenticate()
        {
            try
            {
                var authenticationResult = await WebAuthenticator.AuthenticateAsync(
                    new Uri($"https://github.com/login/oauth/authorize?client_id={Configuration.GitHub.ClientId}&scope=user&redirect_uri=myapp://"),
                    new Uri("myapp://"));

                //code
                var code = authenticationResult.Properties["code"];

                // POST https://github.com/login/oauth/access_token                
                var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, $"https://github.com/login/oauth/access_token?code={code}&client_id={Configuration.GitHub.ClientId}&client_secret={Configuration.GitHub.ClientSecret}"));
                var content = await response.Content.ReadAsStringAsync();
                return new OperationResponse<string>(OperationResult.Succeeded, content.Split("&")[0].Replace("access_token=", ""));
            }
            catch (Exception e)
            {
                string message = null;

                switch (e)
                {
                    case TaskCanceledException _:
                        message = "User canceled login.";
                        break;
                }

                return new OperationResponse<string>(OperationResult.Failed, null, message ?? e.Message);
            }
        }
    }
}
