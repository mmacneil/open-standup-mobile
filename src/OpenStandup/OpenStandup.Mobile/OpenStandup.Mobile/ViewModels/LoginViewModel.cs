using System;
using System.Threading;
using OpenStandup.Core.Domain.Features.Login.Models;
using OpenStandup.Mobile.Services;
using MediatR;
using System.Threading.Tasks;
using OpenStandup.Core.Interfaces;
using OpenStandup.Mobile.Infrastructure.Services;
using OpenStandup.Mobile.Interfaces;
using Xamarin.Forms;


namespace OpenStandup.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAppContext _appContext;
        private readonly IDialogProvider _dialogProvider;
        private readonly JobService _jobService;
        private readonly IMediator _mediator;
        private readonly INavigator _navigator;

        private bool _canLogin = true;

        public bool CanLogin
        {
            get => _canLogin;
            set => SetProperty(ref _canLogin, value);
        }

        public LoginViewModel(IAppContext appContext, IDialogProvider dialogProvider, JobService jobService, IMediator mediator, INavigator navigator)
        {
            _appContext = appContext;
            _dialogProvider = dialogProvider;
            _jobService = jobService;
            _mediator = mediator;
            _navigator = navigator;
        }

        public async Task Login()
        {
            IsBusy = true;
            CanLogin = false;
            StatusText = "Signing in with GitHub...";

            // Call the Login UseCase, on success we'll load the application shell, error handling is performed by the presenter
            if ((await _mediator.Send(new LoginRequest())).Succeeded)
            {
                StatusText = "Signed in, getting things ready!";
                _jobService.Start(new CancellationToken());
            }
        }

        public void Initialize(bool logout)
        {
            _jobService.TimerFired += JobServiceOnTimerFired;
            if (!logout) return;
            Reset();
        }

        public void Uninitialize()
        {
            _jobService.TimerFired -= JobServiceOnTimerFired;
        }

        private void JobServiceOnTimerFired(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                // Final check to make sure we're in a valid state before proceeding
                if (_appContext.User == null)
                {
                    await _dialogProvider.DisplayAlert("Oops", "There was an error starting up, please login to try again.", "Ok");
                    Reset();
                    return;
                }

                await _navigator.GoTo("///main");
            });
        }

        private void Reset()
        {
            IsBusy = false;
            CanLogin = true;
            StatusText = "";
        }
    }
}
