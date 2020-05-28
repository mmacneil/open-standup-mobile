using CleanXF.Core.Domain.Features.Authenticate.Models;
using CleanXF.Core.Interfaces.Authentication;
using MediatR;
using System.Threading.Tasks;

namespace CleanXF.Mobile.ViewModels
{
    public class InitializeViewModel : BaseViewModel
    {         
        //private readonly ISessionRepository _sessionRepository;
        private readonly IMediator _mediator;

        public InitializeViewModel(IMediator mediator)
        {           
            _mediator = mediator;
        }

        public async Task Initialize()
        {
            IsBusy = true;
            await _mediator.Send(new AuthenticationRequest());          
        }
    }
}
