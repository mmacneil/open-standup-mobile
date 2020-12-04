using System.Threading.Tasks;
using MediatR;
using OpenStandup.Core.Domain.Events;
using OpenStandup.Core.Domain.Features.Logout.Models;
using OpenStandup.Core.Interfaces;
using OpenStandup.Mobile.Interfaces;
using OpenStandup.Mobile.Services;


namespace OpenStandup.Mobile.Presenters
{
    public class UnauthorizedPresenter : IOutputPort<Unauthorized>
    {
        private readonly IMediator _mediator;
        private readonly INavigator _navigator;
        private readonly IDialogProvider _dialogProvider;

        public UnauthorizedPresenter(IDialogProvider dialogProvider, IMediator mediator, INavigator navigator)
        {
            _mediator = mediator;
            _navigator = navigator;
            _dialogProvider = dialogProvider;
        }

        public async Task Handle(Unauthorized response)
        {
            await _dialogProvider.DisplayAlert("Unauthorized", $"msg: \"{response.ResponseMessage.ReasonPhrase}\" \nPlease login to continue.", "Ok");
            await _mediator.Send(new LogoutRequest()).ConfigureAwait(false);
            await _navigator.GoTo("///login?logout=1").ConfigureAwait(false);
        }
    }
}
