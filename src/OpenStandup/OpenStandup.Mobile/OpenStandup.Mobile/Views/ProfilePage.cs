using Autofac;
using OpenStandup.Core.Interfaces;
using OpenStandup.Mobile.ViewModels;
using OpenStandup.Mobile.Controls;
using Xamarin.Forms;


namespace OpenStandup.Mobile.Views
{
    public class ProfilePage : ContentPage
    {
        private readonly IAppContext _appContext = App.Container.Resolve<IAppContext>();
        private readonly ProfileViewModel _viewModel = App.Container.Resolve<ProfileViewModel>();
        private readonly ProfileLayout _profileLayout;

        public ProfilePage()
        {
            Title = "My Profile";

            _profileLayout = new ProfileLayout(false)
            {
                Margin = new Thickness(0, 20, 0, 0)
            };

            Content = _profileLayout;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.SelectedGitHubId = _appContext.User.Id;
            await _viewModel.Initialize().ConfigureAwait(false);
            _profileLayout.BindingContext = _viewModel;
            _profileLayout.BindStats(_viewModel.StatModels);
        }
    }
}

