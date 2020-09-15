using Autofac;
using CleanXF.Mobile.ViewModels;
using Xamarin.Forms;

namespace CleanXF.Mobile.Views
{
    public class ProfilePage : ContentPage
    {
        private readonly ProfileViewModel _viewModel = App.Container.Resolve<ProfileViewModel>();

        public ProfilePage()
        {
            BindingContext = _viewModel;

            Title = "My Profile";

            Grid grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(2, GridUnitType.Star) }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition()
                }
            };

            var image = new Image
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center             
            };

            image.SetBinding(Image.SourceProperty, nameof(ProfileViewModel.AvatarUrl));

            StackLayout imageLayout = new StackLayout { Padding = 40 };

            var frame = new Frame
            {
                CornerRadius = 75,
                HeightRequest = 150,
                WidthRequest = 150,
                Padding = 0,
                HorizontalOptions = LayoutOptions.Center,
                IsClippedToBounds = true,
                Content = image
            };

            imageLayout.Children.Add(frame);
            Content = imageLayout;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.Initialize().ConfigureAwait(false);
        }
    }
}
 
 