using CleanXF.Core.Domain.Features.Authenticate.Models;
using CleanXF.Mobile.Presenters;
using CleanXF.Mobile.Services;
using MediatR;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CleanXF.Mobile.ViewModels
{
    public class InitializeViewModel : BaseViewModel
    {
        private readonly IMediator _mediator;
        private readonly INavigator _navigator;

        private string _statusText;
        public string StatusText
        {
            get { return _statusText; }
            set { SetProperty(ref _statusText, value); }
        }

        public InitializeViewModel(IMediator mediator, INavigator navigator)
        {
            _mediator = mediator;
            _navigator = navigator;
        }

        public async Task Initialize()
        {
            await Login();
        }

        public async Task Login()
        {
            IsBusy = true;
            StatusText = "Signing in...";

            // Call the Login UseCase, on success we'll load the application shell, error handling
            // is performed by the presenter
            if (await _mediator.Send(new AuthenticationRequest(new AuthenticationPresenter(this))))
            {
                _navigator.LoadShell();
            }
        }
    }
}
