using System;
using Autofac;
using OpenStandup.Mobile.Controls;
using OpenStandup.Mobile.ViewModels;
using Xamarin.Forms;


namespace OpenStandup.Mobile.Views
{
    [QueryProperty("Logout", "logout")]
    public class LoginPage : ContentPage
    {
        private string _logout;
        public string Logout
        {
            get => _logout;
            set => _logout = Uri.UnescapeDataString(value);
        }

        private readonly LoginViewModel _viewModel = App.Container.Resolve<LoginViewModel>();

        public LoginPage()
        {
            BindingContext = _viewModel;

            Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
            Shell.SetNavBarIsVisible(this, false);
            Shell.SetPresentationMode(this, PresentationMode.ModalAnimated);
            Shell.SetTabBarIsVisible(this, false);
            Visual = VisualMarker.Material;

            var grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(40, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(60, GridUnitType.Star) }
                }
            };

            grid.Children.Add(new Image
            {
                Source = "github_security_logo.png",
                HeightRequest = 200
            });

            var activityIndicator = new ActivityIndicator();
            activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, nameof(BaseViewModel.IsBusy));

            var statusLabel = new Label { HorizontalTextAlignment = TextAlignment.Center };
            statusLabel.SetBinding(Label.TextProperty, nameof(LoginViewModel.StatusText));

            var button = new NavigationButton { Text = "Sign in with GitHub", WidthRequest = 120 };
            button.SetBinding(IsVisibleProperty, nameof(LoginViewModel.CanLogin), BindingMode.OneWay);
            button.Clicked += Login_Clicked;

            grid.Children.Add(new StackLayout
            {
                Padding = new Thickness(0, 20),
                Children = { activityIndicator, statusLabel, button }
            }, 0, 1);

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Margin = 75,
                Children = { grid }
            };
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            await _viewModel.Login();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.Initialize(Logout == "1");
        }
    }
}
