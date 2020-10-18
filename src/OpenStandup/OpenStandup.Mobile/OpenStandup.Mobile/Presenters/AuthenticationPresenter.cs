using OpenStandup.Core.Domain.Features.Authenticate.Models;
using OpenStandup.Core.Interfaces;
using OpenStandup.Mobile.ViewModels;


namespace OpenStandup.Mobile.Presenters
{
    public class AuthenticationPresenter : IOutputPort<AuthenticationResponse>
    {
        private readonly LoginViewModel _viewModel;

        public AuthenticationPresenter(LoginViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Handle(AuthenticationResponse response)
        {
            if (response.Succeeded)
            {
                _viewModel.StatusText = "Signed in!";
            }
            else
            {
                _viewModel.StatusText = response.ErrorText;
                _viewModel.CanLogin = true;
                _viewModel.IsBusy = false;
            }
        }
    }
}
