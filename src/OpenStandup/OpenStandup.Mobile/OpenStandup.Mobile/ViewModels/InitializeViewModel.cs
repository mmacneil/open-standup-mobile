using System.Threading.Tasks;
using OpenStandup.Core.Interfaces.Apis;
using OpenStandup.Core.Interfaces.Data.Repositories;
using OpenStandup.Mobile.Infrastructure.Interfaces;
using OpenStandup.Mobile.Services;


namespace OpenStandup.Mobile.ViewModels
{
    public class InitializeViewModel : BaseViewModel
    {
        private string _status;
        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        private bool _failed;
        public bool Failed
        {
            get => _failed;
            set => SetProperty(ref _failed, value);
        }

        private readonly ISecureDataRepository _secureDataRepository;
        private readonly INavigator _navigator;
        private readonly IConfigurationLoader _configurationLoader;
        private readonly IOpenStandupApi _openStandupApi;
     
        
        public InitializeViewModel(ISecureDataRepository secureDataRepository, INavigator navigator, IConfigurationLoader configurationLoader, IOpenStandupApi openStandupApi)
        {
            _secureDataRepository = secureDataRepository;
            _navigator = navigator;
            _configurationLoader = configurationLoader;
            _openStandupApi = openStandupApi;
        }

        public async Task Initialize()
        {
            Failed = false;
            IsBusy = true;
            Status = "Loading...";

            try
            {
                if (!await _configurationLoader.TryLoad()/*.ConfigureAwait(false)*/)
                {
                    Failed = true;
                    Status = "Startup failed, check connection and try again.";
                    return;
                }

                // If we have an access token and it's still valid then proceed to shell, otherwise route to login
                var accessToken = await _secureDataRepository.GetPersonalAccessToken().ConfigureAwait(false);
                if (!string.IsNullOrEmpty(accessToken) && (await _openStandupApi.ValidateGitHubAccessToken(accessToken)/*.ConfigureAwait(false)*/).Succeeded)
                {
                    await _navigator.GoTo("///main");
                }
                else
                {
                    await _navigator.GoTo("///login");
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}