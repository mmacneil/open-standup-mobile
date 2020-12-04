using System.Linq;
using System.Threading.Tasks;
using OpenStandup.Core.Interfaces;
using OpenStandup.Mobile.ViewModels;
using OpenStandup.SharedKernel.Extensions;
using Vessel;

namespace OpenStandup.Mobile.Presenters
{
    public class LoginProfileUpdatePresenter : IOutputPort<Dto<bool>>
    {
        private readonly LoginViewModel _viewModel;

        public LoginProfileUpdatePresenter(LoginViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public Task Handle(Dto<bool> response)
        {
            if (!response.Succeeded)
            {
                _viewModel.StatusText = response.Errors.First().Truncate();
                _viewModel.CanLogin = true;
                _viewModel.IsBusy = false;
            }
           
            return Task.CompletedTask;
        }
    }
}
