using System;
using System.Threading.Tasks;
using CleanXF.Mobile.Infrastructure.Interfaces;
using Xamarin.Essentials;

namespace CleanXF.Mobile.Infrastructure.Data.Repositories
{
    public class SecureDataRepository : ISecureDataRepository
    {
        private const string GitHubClientId = "github_client_id", GitHubClientSecret = "github_client_secret";

        public async Task<string> GetGitHubClientId()
        {
            return await GetSecureStorageValue(GitHubClientId);
        }

        public async Task<string> GetGitHubClientSecret()
        {
            return await GetSecureStorageValue(GitHubClientSecret);
        }

        public async Task SetGitHubClientId(string value)
        {
            await SetSecureStorageValue(GitHubClientId, value);
        }

        public async Task SetGitHubClientSecret(string value)
        {
            await SetSecureStorageValue(GitHubClientSecret, value);
        }

        private static async Task SetSecureStorageValue(string key, string value)
        {
            try
            {
                await SecureStorage.SetAsync(key, value);
            }
            catch (Exception)
            {
                // Possible that device doesn't support secure storage on device.
            }
        }

        private static async Task<string> GetSecureStorageValue(string key)
        {
            try
            {
                return await SecureStorage.GetAsync(key);
            }
            catch (Exception)
            {
                // Possible that device doesn't support secure storage on device.
            }

            return null;
        }
    }
}