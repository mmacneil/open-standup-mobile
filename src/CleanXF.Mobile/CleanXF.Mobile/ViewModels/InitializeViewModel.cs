using CleanXF.Core.Domain.Features.Authenticate.Models;
using CleanXF.Mobile.Services;
using MediatR;
using System.Threading.Tasks;

namespace CleanXF.Mobile.ViewModels
{
    public class InitializeViewModel : BaseViewModel
    {
        private readonly IMediator _mediator;
        private readonly INavigator _navigator;

        public InitializeViewModel(IMediator mediator, INavigator navigator)
        {
            _mediator = mediator;
            _navigator = navigator;
        }

        public async Task Initialize()
        {
            IsBusy = true;            

            if(await Login())
            {                
                _navigator.LoadShell();
            }        
        }

        public async Task<bool> Login()
        {
            var authenticationResponse = await _mediator.Send(new AuthenticationRequest());

            if (!authenticationResponse.Succeeded)
            {
                ErrorText = authenticationResponse.ErrorText;
                IsBusy = false;
                return false;
            }

            return true;
        }
    }
}
