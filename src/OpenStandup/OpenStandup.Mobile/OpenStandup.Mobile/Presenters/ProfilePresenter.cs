using System.Linq;
using System.Threading.Tasks;
using OpenStandup.Core.Domain.Features.Profile.Models;
using OpenStandup.Core.Interfaces;
using OpenStandup.Mobile.ViewModels;
using Vessel;

namespace OpenStandup.Mobile.Presenters
{
    public class ProfilePresenter : IOutputPort<Dto<GetProfileResponse>>
    {
        private readonly ProfileViewModel _viewModel;

        public ProfilePresenter(ProfileViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public Task Handle(Dto<GetProfileResponse> response)
        {
            if (response.Succeeded)
            {
                _viewModel.CanFollow = !response.Payload.IsFollowing && response.Payload.CanFollow;
                _viewModel.IsFollowing = response.Payload.IsFollowing;
                _viewModel.SetData(response.Payload.User.Payload);
            }
            else
            {
                _viewModel.IsBusy = false;
                _viewModel.StatusText = response.Errors.FirstOrDefault();
            }

            _viewModel.ShowActions = !string.IsNullOrEmpty(_viewModel.SelectedGitHubId);
            _viewModel.SelectedGitHubId = null; // fix me so "local" view doesn't need this reset
            return Task.CompletedTask;
        }
    }
}
