using Autofac;
using CleanXF.Mobile.ViewModels;
using Xamarin.Forms;

namespace CleanXF.Mobile.Views
{
    public class InitializePage : ContentPage
    {

        private InitializeViewModel _viewModel { get; set; } = App.Container.Resolve<InitializeViewModel>();

        public InitializePage()
        {
            Shell.SetNavBarIsVisible(this, false);

            var rootLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center
            };

            rootLayout.Children.Add(new ActivityIndicator { IsRunning = true });
            rootLayout.Children.Add(new Label { HorizontalTextAlignment = TextAlignment.Center, Text = "Starting up..." });
            Content = rootLayout;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.Initialize();
            //await _viewModel.Initialize();
        }
    }
}


