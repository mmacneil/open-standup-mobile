using System.Linq;
using System.Threading.Tasks;
using OpenStandup.Core.Domain.Features.Profile.Models;
using OpenStandup.Core.Interfaces;
using OpenStandup.Mobile.Interfaces;
using Rg.Plugins.Popup.Contracts;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Presenters
{
    public class UpdateFollowerPresenter : IOutputPort<UpdateFollowerResponse>
    {
        private readonly IDialogProvider _dialogProvider;
        private readonly IPopupNavigation _popupNavigation;
        private readonly IToastService _toastService;

        public UpdateFollowerPresenter(IDialogProvider dialogProvider, IPopupNavigation popupNavigation, IToastService toastService)
        {
            _dialogProvider = dialogProvider;
            _popupNavigation = popupNavigation;
            _toastService = toastService;
        }

        public Task Handle(UpdateFollowerResponse response)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (response.ApiResponse.Succeeded)
                {
                    _toastService.Show(response.Request.Follow ? $"You're following {response.Request.Login}" : $"You've unfollowed {response.Request.Login}");
                    await _popupNavigation.PopAsync();
                }
                else
                {
                    await _dialogProvider.DisplayAlert("Error",
                        $"Operation failed {response.ApiResponse.Errors.FirstOrDefault()}", "Ok");
                }
            });

            return Task.CompletedTask;
        }
    }
}

