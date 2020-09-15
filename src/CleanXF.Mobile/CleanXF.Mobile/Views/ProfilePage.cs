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

            Grid header = new Grid
            {                
               // BackgroundColor = Color.Green,                
                ColumnDefinitions =
                {
                    new ColumnDefinition() { Width = new GridLength(40, GridUnitType.Star) },
                    new ColumnDefinition() { Width = new GridLength(60, GridUnitType.Star) },
                }
            };

            var image = new Image
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            image.SetBinding(Image.SourceProperty, nameof(ProfileViewModel.AvatarUrl));

            StackLayout imageLayout = new StackLayout
            {
                Children =
                {
                    new Frame
                    {
                        CornerRadius = 50,
                        HeightRequest = 100,
                        WidthRequest = 100,
                        Padding = 0,
                        HorizontalOptions = LayoutOptions.Center,
                        IsClippedToBounds = true,
                        Content = image
                    }
                }
            };

            header.Children.Add(imageLayout);
            header.Children.Add(new StackLayout
            {
                // VerticalOptions = LayoutOptions.Center,

                Children = 
                {
                    new Label { Text = "mmacneil", FontSize = 26, FontAttributes = FontAttributes.Bold },
                    new Label { Text = "Halifax, NS", FontSize = 20}
                }
            }, 1, 0);

            Content = header;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.Initialize().ConfigureAwait(false);
        }
    }
}

