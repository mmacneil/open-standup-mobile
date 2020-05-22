using CleanXF.Core.Interfaces.Authentication;
using Polly;
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

        public async Task<string> Authenticate()
        {           
            try
            {
                var authenticationResult = await WebAuthenticator.AuthenticateAsync(
                    new Uri($"https://github.com/login/oauth/authorize?client_id={Configuration.GitHub.ClientId}&redirect_uri=myapp://"),
                    new Uri("myapp://"));

                //code
                var code = authenticationResult.Properties["code"];

                // var accessToken = authResult?.AccessToken;
                // POST https://github.com/login/oauth/access_token
                
                var msg = new HttpRequestMessage(HttpMethod.Post, $"https://github.com/login/oauth/access_token?code={code}&client_id={Configuration.GitHub.ClientId}&client_secret={Configuration.GitHub.ClientSecret}");

                var response = await _httpClient.SendAsync(msg);
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch(Exception e)
            {
                return null;
            }          
        }
    }
}
