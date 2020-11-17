using System.Linq;
using OpenStandup.Core.Interfaces;
using OpenStandup.Mobile.ViewModels;
using OpenStandup.SharedKernel.Extensions;
using Vessel;

namespace OpenStandup.Mobile.Presenters
{
    public class LoginGitHubProfileUpdatePresenter : IOutputPort<Dto<bool>>
    {
        private readonly LoginViewModel _viewModel;

        public LoginGitHubProfileUpdatePresenter(LoginViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Handle(Dto<bool> response)
        {
            if (response.Succeeded) return;
            _viewModel.StatusText = response.Errors.First().Truncate();
            _viewModel.CanLogin = true;
            _viewModel.IsBusy = false;
        }
    }
}
