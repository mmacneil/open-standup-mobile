using System;
using System.Threading.Tasks;
using OpenStandup.Core.Interfaces.Data.Repositories;
using Xamarin.Essentials;

namespace OpenStandup.Mobile.Infrastructure.Data.Repositories
{
    public class SecureDataRepository : ISecureDataRepository
    {
        private const string GitHubClientId = "github_client_id", GitHubClientSecret = "github_client_secret", PersonalAccessToken = "pat";

        public Task<string> GetGitHubClientId()
        {
            return GetSecureStorageValue(GitHubClientId);
        }

        public Task<string> GetGitHubClientSecret()
        {
            return GetSecureStorageValue(GitHubClientSecret);
        }

        public Task<string> GetPersonalAccessToken()
        {
            return GetSecureStorageValue(PersonalAccessToken);
        }

        public Task SetGitHubClientId(string value)
        {
            return SetSecureStorageValue(GitHubClientId, value);
        }

        public Task SetGitHubClientSecret(string value)
        {
            return SetSecureStorageValue(GitHubClientSecret, value);
        }

        public Task SetPersonalAccessToken(string value)
        {
            return SetSecureStorageValue(PersonalAccessToken, value);
        }

        public bool RemovePersonalAccessToken()
        {
            return RemoveSecureStorageValue(PersonalAccessToken);
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

        private static Task<string> GetSecureStorageValue(string key)
        {
            try
            {
                return SecureStorage.GetAsync(key);
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