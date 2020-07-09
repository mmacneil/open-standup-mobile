using CleanXF.Core.Domain.Features.Authenticate.Models;
using CleanXF.Core.Interfaces.Data.Repositories;
using CleanXF.Mobile.Presenters;
using CleanXF.Mobile.Services;
using MediatR;
using System.Threading.Tasks;


namespace CleanXF.Mobile.ViewModels
{
    public class InitializeViewModel : BaseViewModel
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly INavigator _navigator;
        public InitializeViewModel(ISessionRepository sessionRepository, INavigator navigator)
        {
            _sessionRepository = sessionRepository;
            _navigator = navigator;
        }

        public async void Initialize()
        {
            // If we have an access token we're considered logged in, otherwise route to login
            //if(await _sessionRepository.HasAccessToken())
           //{
            //    bool here = true;
            //}
            //else
           // {
                await _navigator.GoTo("///login");
           // }
        }

        public async Task Login()
        {
            /*
            IsBusy = true;
            ShowLogin = false;
            StatusText = "Signing in with GitHub...";

            // Call the Login UseCase, on success we'll load the application shell, error handling
            // is performed by the presenter
            if (await _mediator.Send(new AuthenticationRequest(new AuthenticationPresenter(this))))
            {
                await Task.Delay(1);  // UI doesn't completely update on android for some reason
                //_navigator.LoadShell();
            }*/

            /*  var isAuthenticated = await this.identityService.VerifyRegistration();
                if (isAuthenticated)
                {
                    await this.routingService.NavigateTo("///main");
                }
                else
                {
                    await this.routingService.NavigateTo("///login");
                }*/
        }
    }
}
