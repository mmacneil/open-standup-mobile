using CleanXF.Mobile.Converters;
using CleanXF.Mobile.ViewModels;
using Xamarin.Forms;

namespace CleanXF.Mobile.Views
{
    public class InitializePage : ContentPage
    {
        private InitializeViewModel _viewModel;

        public InitializePage()
        {
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
            statusLabel.SetBinding(Label.TextProperty, nameof(InitializeViewModel.StatusText));

            var loginLayout = new StackLayout
            {
                Padding = new Thickness(0, 15)
            };

            loginLayout.Children.Add(activityIndicator);
            loginLayout.Children.Add(statusLabel);

            grid.Children.Add(loginLayout, 0, 1);


            /*
          
          

            var button = new Button { Text = "Login", WidthRequest = 120 };
            button.SetBinding(IsVisibleProperty, nameof(BaseViewModel.ErrorText), BindingMode.OneWay, new StringBooleanConverter());
            button.Clicked += Login_Clicked;

            rootLayout.Children.Add(activityIndicator);
            rootLayout.Children.Add(statusLabel);
            rootLayout.Children.Add(button);*/

            rootLayout.Children.Add(grid);

            Content = rootLayout;
        }

        private async void Login_Clicked(object sender, System.EventArgs e)
        {
            _viewModel.ErrorText = "";
            await _viewModel.Login();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _viewModel = (InitializeViewModel)BindingContext;
            await _viewModel.Initialize();
        }
    }
}
