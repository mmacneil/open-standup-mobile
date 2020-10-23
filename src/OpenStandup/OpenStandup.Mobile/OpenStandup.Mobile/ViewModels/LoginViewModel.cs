﻿using OpenStandup.Core.Domain.Features.Authenticate.Models;
using OpenStandup.Mobile.Presenters;
using OpenStandup.Mobile.Services;
using MediatR;
using System.Threading.Tasks;


namespace OpenStandup.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IMediator _mediator;
        private readonly INavigator _navigator;

        private string _statusText;
        public string StatusText
        {
            get => _statusText;
            set => SetProperty(ref _statusText, value);
        }

        private bool _canLogin = true;
        public bool CanLogin
        {
            get => _canLogin;
            set => SetProperty(ref _canLogin, value);
        }

        public LoginViewModel(IMediator mediator, INavigator navigator)
        {
            _mediator = mediator;
            _navigator = navigator;
        }

        public async Task Login()
        {
            IsBusy = true;
            CanLogin = false;
            StatusText = "Signing in with GitHub...";

            // Call the Login UseCase, on success we'll load the application shell, error handling
            // is performed by the presenter
            if (await _mediator.Send(new AuthenticationRequest(new AuthenticationPresenter(this))))
            {
                await Task.Delay(1);  // UI doesn't completely update on android for some reason
                await _navigator.GoTo("///main");
            }
        }
    }
}
