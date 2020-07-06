using CleanXF.Core.Domain.Features.Authenticate.Models;
using CleanXF.Mobile.Presenters;
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

            if (await Login())
            {
                _navigator.LoadShell();
            }
        }

        public async Task<bool> Login()
        {
            return await _mediator.Send(new AuthenticationRequest(new AuthenticationPresenter(this)));
        }
    }
}
