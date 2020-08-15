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

            /*Grid grid = new Grid();

            var image = new Image();
            image.SetBinding(Image.SourceProperty, nameof(ProfileViewModel.AvatarUrl));

            grid.Children.Add(image);*/

            Content = grid;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.Initialize().ConfigureAwait(false);
        }
    }
}
