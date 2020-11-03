using System.Threading.Tasks;
using OpenStandup.Core.Interfaces.Apis;
using OpenStandup.Core.Interfaces.Data.Repositories;
using OpenStandup.Mobile.Infrastructure.Data;
using OpenStandup.Mobile.Infrastructure.Interfaces;


namespace OpenStandup.Mobile.Infrastructure.Configuration
{
    public class ConfigurationLoader : IConfigurationLoader
    {
        private readonly IOpenStandupApi _openStandupApi;
        private readonly AppDb _appDb;
        private readonly ISecureDataRepository _secureDataRepository;

        public ConfigurationLoader(AppDb appDb, IOpenStandupApi openStandupApi, ISecureDataRepository secureDataRepository)
        {
            _appDb = appDb;
            _openStandupApi = openStandupApi;
            _secureDataRepository = secureDataRepository;
        }

        public async Task<bool> TryLoad()
        {
            var configurationResponse = await _openStandupApi.GetConfiguration();

            if (!configurationResponse.Succeeded) return false;

            await _secureDataRepository.SetGitHubClientId(configurationResponse.Value.GitHubClientId);
            await _secureDataRepository.SetGitHubClientSecret(configurationResponse.Value.GitHubClientSecret);

            return await _appDb.AsyncDb.InsertOrReplaceAsync(new Data.Model.Configuration
            {
                Version = configurationResponse.Value.Version,
                Created = configurationResponse.Value.Created
            }) == 1;
        }
    }
}

