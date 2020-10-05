using System.Threading.Tasks;
using CleanXF.Core.Interfaces.Data.Repositories;
using CleanXF.Mobile.Infrastructure.Interfaces;
using CleanXF.Mobile.Services;

namespace CleanXF.Mobile.ViewModels
{
    public class InitializeViewModel : BaseViewModel
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly INavigator _navigator;
        private readonly IConfigurationLoader _configurationLoader;
        public InitializeViewModel(ISessionRepository sessionRepository, INavigator navigator, IConfigurationLoader configurationLoader)
        {
            _sessionRepository = sessionRepository;
            _navigator = navigator;
            _configurationLoader = configurationLoader;
        }

        public async void Initialize()
        {
            await Task.Delay(700);

            await _configurationLoader.TryLoad();

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
    }
}