using System.Linq;
using System.Threading.Tasks;
using OpenStandup.Core.Interfaces;
using OpenStandup.Mobile.ViewModels;
using Vessel;


namespace OpenStandup.Mobile.Presenters
{
    public class LoginPresenter : IOutputPort<Dto<string>>
    {
        private readonly LoginViewModel _viewModel;

        public LoginPresenter(LoginViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public Task Handle(Dto<string> response)
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

            return Task.CompletedTask;
        }
    }
}
