using System.Linq;
using OpenStandup.Core.Interfaces;
using OpenStandup.Mobile.ViewModels;
using OpenStandup.SharedKernel;

namespace OpenStandup.Mobile.Presenters
{
    public class SaveGitHubProfilePresenter : IOutputPort<Result<bool>>
    {
        private readonly LoginViewModel _viewModel;

        public SaveGitHubProfilePresenter(LoginViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Handle(Result<bool> response)
        {
            if (response.Succeeded) return;
            _viewModel.StatusText = response.Errors.First();
            _viewModel.CanLogin = true;
            _viewModel.IsBusy = false;
        }
    }
}
