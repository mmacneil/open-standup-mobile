using CleanXF.Mobile.ViewModels;
using Xamarin.Forms;

namespace CleanXF.Mobile.Views
{

    public class LoginPagexxx : ContentPage
    {
        private LoginViewModel _viewModel;

        public LoginPagexxx()
        {
            //Shell.SetNavBarIsVisible(this, false);
            Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
            // Shell.SetTabBarIsVisible(this, false);

            /*  Shell.NavBarIsVisible="True"
             Shell.FlyoutBehavior="Disabled"
             Shell.TabBarIsVisible="False"*/



            var rootLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Margin = 75
            };

            var grid = new Grid
            {
                RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(40, GridUnitType.Star) },             
                new RowDefinition { Height = new GridLength(60, GridUnitType.Star) }
            }}; 

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
            button.SetBinding(IsVisibleProperty, nameof(LoginViewModel.ShowLogin), BindingMode.OneWay);
            button.Clicked += Login_Clicked;

            var loginLayout = new StackLayout
            {
                Padding = new Thickness(0, 20)
            };

            loginLayout.Children.Add(activityIndicator);
            loginLayout.Children.Add(statusLabel);
            loginLayout.Children.Add(button);

            grid.Children.Add(loginLayout, 0, 1);        

            rootLayout.Children.Add(grid);

            Content = rootLayout;
        }

        private async void Login_Clicked(object sender, System.EventArgs e)
        {          
            await _viewModel.Login();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
           // _viewModel = (LoginViewModel)BindingContext;
          //  await _viewModel.Initialize();
        }
    }
}
