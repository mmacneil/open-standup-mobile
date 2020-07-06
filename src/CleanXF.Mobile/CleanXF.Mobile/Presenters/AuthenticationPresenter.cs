using CleanXF.Core.Domain.Features.Authenticate.Models;
using CleanXF.Core.Interfaces;
using CleanXF.Mobile.ViewModels;


namespace CleanXF.Mobile.Presenters
{
    public class AuthenticationPresenter : IOutputPort<AuthenticationResponse>
    {
        private readonly InitializeViewModel _viewModel;

        public AuthenticationPresenter(InitializeViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Handle(AuthenticationResponse response)
        {
            if (!response.Succeeded)
            {
                _viewModel.ErrorText = response.ErrorText;
                _viewModel.IsBusy = false;
            }
        }
    }
}
