using System.Threading.Tasks;
using CleanXF.Core.Interfaces.Apis;
using CleanXF.Mobile.Infrastructure.Data;
using CleanXF.Mobile.Infrastructure.Interfaces;


namespace CleanXF.Mobile.Infrastructure.Configuration
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

            await _secureDataRepository.SetGitHubClientId(configurationResponse.Payload.GitHubClientId);
            await _secureDataRepository.SetGitHubClientSecret(configurationResponse.Payload.GitHubClientSecret);

            return await _appDb.AsyncDb.InsertOrReplaceAsync(new Data.Model.Configuration
            {
                Version = configurationResponse.Payload.Version,
                Created = configurationResponse.Payload.Created
            }) == 1;
        }
    }
}

