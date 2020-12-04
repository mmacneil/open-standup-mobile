using System.Linq;
using System.Threading.Tasks;
using OpenStandup.Core.Domain.Features.Posts.Models;
using OpenStandup.Core.Interfaces;
using OpenStandup.Mobile.Interfaces;
using OpenStandup.Mobile.Services;
using OpenStandup.Mobile.ViewModels;


namespace OpenStandup.Mobile.Presenters
{
    public class PublishPostPresenter : IOutputPort<PublishPostResponse>
    {
        private readonly EditPostViewModel _viewModel;
        private readonly IDialogProvider _dialogProvider;
        private readonly INavigator _navigator;

        public PublishPostPresenter(EditPostViewModel viewModel, IDialogProvider dialogProvider, INavigator navigator)
        {
            _viewModel = viewModel;
            _dialogProvider = dialogProvider;
            _navigator = navigator;
        }

        public async Task Handle(PublishPostResponse response)
        {
            if (!response.ApiResponse.Succeeded)
            {
                await _dialogProvider.DisplayAlert("Post Failed", $"msg: {response.ApiResponse.Errors.FirstOrDefault()} \nPlease check your connection and try again.", "Ok").ConfigureAwait(false);
            }
            else
            {
                _viewModel.Text = "";
                _viewModel.PhotoPath = "";
                await _navigator.PopAsync();
            }
        }
    }
}