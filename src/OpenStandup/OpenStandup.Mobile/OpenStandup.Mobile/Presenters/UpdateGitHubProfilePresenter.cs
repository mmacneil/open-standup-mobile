using System.Linq;
using OpenStandup.Core.Interfaces;
using OpenStandup.Mobile.ViewModels;
using Vessel;

namespace OpenStandup.Mobile.Presenters
{
    public class UpdateGitHubProfilePresenter : IOutputPort<Dto<bool>>
    {
        private readonly LoginViewModel _viewModel;

        public UpdateGitHubProfilePresenter(LoginViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Handle(Dto<bool> response)
        {
            if (response.Succeeded) return;
            _viewModel.StatusText = response.Errors.First();
            _viewModel.CanLogin = true;
            _viewModel.IsBusy = false;
        }
    }
}
