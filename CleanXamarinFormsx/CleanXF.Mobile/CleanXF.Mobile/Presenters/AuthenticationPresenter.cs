using CleanXF.Core.Domain.Features.Authenticate.Models;
using CleanXF.Core.Interfaces;
using CleanXF.Mobile.ViewModels;


namespace CleanXF.Mobile.Presenters
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
