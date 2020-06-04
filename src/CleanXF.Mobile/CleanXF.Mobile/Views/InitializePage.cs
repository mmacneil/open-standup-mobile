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
            rootLayout.Children.Add(activityIndicator);
            Content = rootLayout;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ((InitializeViewModel)BindingContext).Initialize();
        }
    }
}
