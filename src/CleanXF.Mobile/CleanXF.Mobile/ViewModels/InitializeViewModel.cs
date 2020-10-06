using System.Threading.Tasks;
using CleanXF.Core.Interfaces.Data.Repositories;
using CleanXF.Mobile.Infrastructure.Interfaces;
using CleanXF.Mobile.Services;

namespace CleanXF.Mobile.ViewModels
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

        private readonly ISessionRepository _sessionRepository;
        private readonly INavigator _navigator;
        private readonly IConfigurationLoader _configurationLoader;
        public InitializeViewModel(ISessionRepository sessionRepository, INavigator navigator, IConfigurationLoader configurationLoader)
        {
            _sessionRepository = sessionRepository;
            _navigator = navigator;
            _configurationLoader = configurationLoader;
        }

        public async Task Initialize()
        {
            try
            {
                Failed = false;
                IsBusy = true;
                Status = "Starting up...";

                await Task.Delay(350);

                if (!await _configurationLoader.TryLoad())
                {
                    Failed = true;
                    Status = "Startup failed, check connection and try again.";
                    return;
                }

                // If we have an access token we're considered logged in so proceed to shell, otherwise route to login
                if (await _sessionRepository.HasAccessToken())
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