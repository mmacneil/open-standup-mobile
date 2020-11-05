using System.Linq;
using OpenStandup.Core.Interfaces;
using OpenStandup.Mobile.ViewModels;
using Vessel;


namespace OpenStandup.Mobile.Presenters
{
    public class AuthenticationPresenter : IOutputPort<Dto<string>>
    {
        private readonly LoginViewModel _viewModel;

        public AuthenticationPresenter(LoginViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Handle(Dto<string> response)
        {
            if (response.Succeeded)
            {
                _viewModel.StatusText = "Signed in!";
            }
            else
            {
                _viewModel.StatusText = response.Errors.First();
                _viewModel.CanLogin = true;
                _viewModel.IsBusy = false;
            }
        }
    }
}
