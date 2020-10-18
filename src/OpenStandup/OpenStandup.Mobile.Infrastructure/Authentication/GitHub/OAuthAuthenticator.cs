using OpenStandup.Core.Interfaces.Authentication;
using OpenStandup.SharedKernel;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using OpenStandup.Core.Interfaces;
using Xamarin.Essentials;

namespace OpenStandup.Mobile.Infrastructure.Authentication.GitHub
{
    public class OAuthAuthenticator : IAuthenticator
    {
        private readonly HttpClient _httpClient;
        private readonly IAppSettings _appSettings;

        public OAuthAuthenticator(HttpClient httpClient, IAppSettings appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings;
        }

        public async Task<OperationResponse<string>> Authenticate()
        {
            try
            {
                var authenticationResult = await WebAuthenticator.AuthenticateAsync(
                    new Uri($"https://github.com/login/oauth/authorize?client_id={await _appSettings.GetGitHubClientId()}&scope=user%20public_repo%20repo%20repo_deployment%20repo:status%20read:repo_hook%20read:org%20read:public_key%20read:gpg_key&redirect_uri=myapp://"),
                    new Uri("myapp://"));

                //code
                var code = authenticationResult.Properties["code"];

                // POST https://github.com/login/oauth/access_token                
                var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, $"https://github.com/login/oauth/access_token?code={code}&client_id={await _appSettings.GetGitHubClientId()}&client_secret={await _appSettings.GetGitHubClientSecret()}"));
                var content = await response.Content.ReadAsStringAsync();
                return new OperationResponse<string>(OperationResult.Succeeded, content.Split("&")[0].Replace("access_token=", ""));
            }
            catch (Exception e)
            {
                string message = null;

                if (e is TaskCanceledException)
                {
                    message = "User canceled login.";
                }

                return new OperationResponse<string>(OperationResult.Failed, null, message ?? e.Message);
            }
        }
    }
}









