using Autofac; 
using CleanXF.Mobile.ViewModels; 
using Xamarin.Forms;

namespace CleanXF.Mobile.Views
{
    public class LoginPage : ContentPage
    {
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
                Source = "github_logo.png",
                HeightRequest = 200
            });

            var activityIndicator = new ActivityIndicator();
            activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, nameof(BaseViewModel.IsBusy));

            var statusLabel = new Label { HorizontalTextAlignment = TextAlignment.Center };
            statusLabel.SetBinding(Label.TextProperty, nameof(LoginViewModel.StatusText));

            var button = new Button { Text = "Sign in with GitHub", WidthRequest = 120 };
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

        private async void Login_Clicked(object sender, System.EventArgs e)
        {
            await _viewModel.Login();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

      
    }
}
