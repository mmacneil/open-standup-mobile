using System;
using System.Threading.Tasks;
using CleanXF.Core.Interfaces.Data.Repositories;
using Xamarin.Essentials;

namespace CleanXF.Mobile.Infrastructure.Data.Repositories
{
    public class SecureDataRepository : ISecureDataRepository
    {
        private const string GitHubClientId = "github_client_id", GitHubClientSecret = "github_client_secret", PersonalAccessToken = "pat";

        public async Task<string> GetGitHubClientId()
        {
            return await GetSecureStorageValue(GitHubClientId);
        }

        public async Task<string> GetGitHubClientSecret()
        {
            return await GetSecureStorageValue(GitHubClientSecret);
        }

        public async Task<string> GetPersonalAccessToken()
        {
            return await GetSecureStorageValue(PersonalAccessToken);
        }

        public async Task SetGitHubClientId(string value)
        {
            await SetSecureStorageValue(GitHubClientId, value);
        }

        public async Task SetGitHubClientSecret(string value)
        {
            await SetSecureStorageValue(GitHubClientSecret, value);
        }

        public async Task SetPersonalAccessToken(string value)
        {
            await SetSecureStorageValue(PersonalAccessToken, value);
        }

        public bool RemovePersonalAccessToken()
        {
            return RemoveSecureStorageValue(PersonalAccessToken);
        }

        public async Task<bool> HasAccessToken()
        {
            return !string.IsNullOrEmpty(await GetSecureStorageValue(PersonalAccessToken));
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

        private static bool RemoveSecureStorageValue(string key)
        {
            try
            {
                return SecureStorage.Remove(key);
            }
            catch (Exception)
            {
                // Possible that device doesn't support secure storage on device.
                return false;
            }
        }
    }
}