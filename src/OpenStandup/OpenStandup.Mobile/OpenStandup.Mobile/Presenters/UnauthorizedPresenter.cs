using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OpenStandup.Core.Domain.Events;
using OpenStandup.Core.Domain.Features.Logout.Models;
using OpenStandup.Core.Interfaces;
using OpenStandup.Mobile.Interfaces;
using OpenStandup.Mobile.Services;
using Rg.Plugins.Popup.Contracts;


namespace OpenStandup.Mobile.Presenters
{
    public class UnauthorizedPresenter : IOutputPort<Unauthorized>
    {
        private readonly IDialogProvider _dialogProvider;
        private readonly IMediator _mediator;
        private readonly INavigator _navigator;
        private readonly IPopupNavigation _popupNavigation;


        public UnauthorizedPresenter(IDialogProvider dialogProvider, IMediator mediator, INavigator navigator, IPopupNavigation popupNavigation)
        {
            _mediator = mediator;
            _navigator = navigator;
            _dialogProvider = dialogProvider;
            _popupNavigation = popupNavigation;
        }

        public async Task Handle(Unauthorized response)
        {
            await _dialogProvider.DisplayAlert("Unauthorized", $"msg: \"{response.ResponseMessage.ReasonPhrase}\" \nPlease login to continue.", "Ok");
            await _mediator.Send(new LogoutRequest()).ConfigureAwait(false);

            if (_popupNavigation.PopupStack.Any())
            {
                await _popupNavigation.PopAllAsync();
            }

            await _navigator.GoTo("///login?logout=1").ConfigureAwait(false);
        }
    }
}
