using CleanXF.Core.Domain.Features.Authenticate.Models;
using CleanXF.Mobile.Presenters;
using CleanXF.Mobile.Services;
using MediatR;
using System.Threading.Tasks;


namespace CleanXF.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IMediator _mediator;
        private readonly INavigator _navigator;
        public bool AutoLogin { get; set; } = true;

        private string _statusText;
        public string StatusText
        {
            get { return _statusText; }
            set { SetProperty(ref _statusText, value); }
        }

        private bool _showLogin;
        public bool ShowLogin
        {
            get { return _showLogin; }
            set { SetProperty(ref _showLogin, value); }
        }

        public LoginViewModel(IMediator mediator, INavigator navigator)
        {
            _mediator = mediator;
            _navigator = navigator;
        }        

        public async Task Login()
        {
            IsBusy = true;
            ShowLogin = false;
            StatusText = "Signing in with GitHub...";

            // Call the Login UseCase, on success we'll load the application shell, error handling
            // is performed by the presenter
            if (await _mediator.Send(new AuthenticationRequest(new AuthenticationPresenter(this))))
            {
                await Task.Delay(1);  // UI doesn't completely update on android for some reason
                await _navigator.GoTo("///main");
            }
        }
    }
}
