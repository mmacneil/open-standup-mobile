using CleanXF.Mobile.ViewModels;
using Xamarin.Forms;

namespace CleanXF.Mobile.Views
{
    public class InitializePage : ContentPage
    {
        public InitializePage()
        {
            var rootLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center
            };

            var activityIndicator = new ActivityIndicator();
            activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, nameof(BaseViewModel.IsBusy));

            var statusLabel = new Label { HorizontalTextAlignment = TextAlignment.Center };
            statusLabel.SetBinding(Label.TextProperty, nameof(BaseViewModel.ErrorText));

            rootLayout.Children.Add(activityIndicator);
            rootLayout.Children.Add(statusLabel);

            Content = rootLayout;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ((InitializeViewModel)BindingContext).Initialize();
        }
    }
}
