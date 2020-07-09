using Autofac;
using CleanXF.Mobile.ViewModels;
using Xamarin.Forms;

namespace CleanXF.Mobile.Views
{
    public class InitializePagexxx : ContentPage
    {
        private readonly InitializeViewModel _viewModel = App.Container.Resolve<InitializeViewModel>();

        public InitializePagexxx()
        {
            Shell.SetNavBarIsVisible(this, false);

            var rootLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children = { new ActivityIndicator { IsRunning = true }, new Label { HorizontalTextAlignment = TextAlignment.Center, Text = "Starting up..." } }
            };

            Content = rootLayout;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.Initialize();        
        }
    }
}


