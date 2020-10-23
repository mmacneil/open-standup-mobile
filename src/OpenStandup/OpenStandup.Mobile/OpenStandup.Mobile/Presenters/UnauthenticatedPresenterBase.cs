using System;
using System.Net;
using OpenStandup.Core.Interfaces;
using OpenStandup.Mobile.ViewModels;



namespace OpenStandup.Mobile.Presenters
{
    public class UnauthenticatedPresenter : IOutputPort<HttpStatusCode>
    {
        private readonly LoginViewModel _viewModel;

        public UnauthenticatedPresenter(LoginViewModel viewModel)
        {
            _viewModel = viewModel;
        }

      /*  public void Handle(AuthenticationResponse response)
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
        }*/

        public void Handle(HttpStatusCode response)
        {
            throw new NotImplementedException();
        }
    }
}
