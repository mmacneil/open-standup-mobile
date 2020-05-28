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

                // POST https://github.com/login/oauth/access_token                
                var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, $"https://github.com/login/oauth/access_token?code={code}&client_id={Configuration.GitHub.ClientId}&client_secret={Configuration.GitHub.ClientSecret}"));
                var content = await response.Content.ReadAsStringAsync();
                return content.Split("&")[0].Replace("access_token=", "");
            }
            catch
            {
                return null;
            }
        }
    }
}
