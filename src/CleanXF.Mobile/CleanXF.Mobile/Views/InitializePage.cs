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
                VerticalOptions = LayoutOptions.Center,
                Children = { new ActivityIndicator { IsRunning = true }, new Label { HorizontalTextAlignment = TextAlignment.Center, Text = "Starting up..." } }
            };

            Content = rootLayout;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.Initialize();        
        }
    }
}


