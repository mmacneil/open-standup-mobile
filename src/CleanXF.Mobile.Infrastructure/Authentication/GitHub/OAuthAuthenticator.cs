using CleanXF.Core.Interfaces.Authentication;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CleanXF.Mobile.Infrastructure.Authentication.GitHub
{
    public class OAuthAuthenticator : IAuthenticator
    {
        public async Task Authenticate()
        {
            var authenticationResult = await WebAuthenticator.AuthenticateAsync(
                new Uri($"https://github.com/login/oauth/authorize?client_id={Configuration.GitHub.ClientId}&redirect_uri=myapp://"),
                new Uri("myapp://"));

            //code
            var code = authenticationResult.Properties["code"];
            // var accessToken = authResult?.AccessToken;
            // POST https://github.com/login/oauth/access_token
            var httpClient = new HttpClient();
            var msg = new HttpRequestMessage(HttpMethod.Post, $"https://github.com/login/oauth/access_token?code={code}&client_id={Configuration.GitHub.ClientId}&client_secret={Configuration.GitHub.ClientSecret}");

            var response = await httpClient.SendAsync(msg);

            var content = await response.Content.ReadAsStringAsync();
        }
    }
}
