using Autofac;
using CleanXF.Mobile.ViewModels;
using Xamarin.Forms;

namespace CleanXF.Mobile.Views
{
    public class InitializePage : ContentPage
    {
        private readonly InitializeViewModel _viewModel = App.Container.Resolve<InitializeViewModel>();

        public InitializePage()
        {
            BindingContext = _viewModel;

            Shell.SetNavBarIsVisible(this, false);

            var activityIndicator = new ActivityIndicator { IsRunning = true };
            var statusLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center
            };

            var button = new Button
            {
                Text = "Retry"
            };

            button.Clicked += async (sender, args) =>
            {
                await _viewModel.Initialize();
            };

            activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, nameof(_viewModel.IsBusy));
            statusLabel.SetBinding(Label.TextProperty, nameof(_viewModel.Status));
            button.SetBinding(IsVisibleProperty, nameof(_viewModel.Failed));

            var rootLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Children = { activityIndicator, statusLabel, button }
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


