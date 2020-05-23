using CleanXF.Core.Interfaces.Authentication;
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

                var accessToken = content.Split("&")[0].Replace("access_token=", "");

                return content;
                // access_token=41fb1ff11451cb30fb720d3b9fc6a3b86e66c9fe&scope=&token_type=bearer
            }
            catch
            {
                return null;
            }          
        }
    }
}
