using Autofac;
using CleanXF.Mobile.ViewModels;
using System.Net.NetworkInformation;
using Xamarin.Forms;


namespace CleanXF.Mobile.Views
{
    public class ProfilePage : ContentPage
    {
        private readonly ProfileViewModel _viewModel = App.Container.Resolve<ProfileViewModel>();

        public ProfilePage()
        {
            Padding = new Thickness(5, 20);

            BindingContext = _viewModel;

            Title = "My Profile";

            Grid header = new Grid
            {                       
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(30, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(70, GridUnitType.Star) },
                }
            };

            Image image = new Image
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

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

            Label login = new Label { Style = (Style)Application.Current.Resources["Title"] };
            login.SetBinding(Label.TextProperty, nameof(ProfileViewModel.Login));

            Label location = new Label { Style = (Style)Application.Current.Resources["SubTitle"] };
            location.SetBinding(Label.TextProperty, nameof(ProfileViewModel.Location));

            Label joined = new Label { Style = (Style)Application.Current.Resources["MicroSubTitle"] };
            joined.SetBinding(Label.TextProperty, nameof(ProfileViewModel.Joined));

            header.Children.Add(imageLayout);
            header.Children.Add(new StackLayout
            {
                Children =
                {
                    login,
                    location,
                    joined
                }
            }, 1, 0);

            Grid stats = new Grid
            {
                Margin = new Thickness(0, 20),               
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(25, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(25, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(25, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(25, GridUnitType.Star) }
                },
                RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition()
                }
            };

            Label followers = new Label { Style = (Style)Application.Current.Resources["ProfileStatValue"] };
            followers.SetBinding(Label.TextProperty, nameof(ProfileViewModel.Followers));

            StackLayout followersLayout = new StackLayout
            {
                Children = 
                { 
                    followers, 
                    new Label { Text = "Followers", Style = (Style)Application.Current.Resources["ProfileStatName"] } 
                }
            };

            Label following = new Label { Style = (Style)Application.Current.Resources["ProfileStatValue"] };
            following.SetBinding(Label.TextProperty, nameof(ProfileViewModel.Following));

            StackLayout followingLayout = new StackLayout
            {
                Children =
                {
                    following,
                    new Label { Text = "Following", Style = (Style)Application.Current.Resources["ProfileStatName"] }
                }
            };

            Label repositories = new Label { Style = (Style)Application.Current.Resources["ProfileStatValue"] };
            repositories.SetBinding(Label.TextProperty, nameof(ProfileViewModel.Repositories));

            StackLayout repositoriesLayout = new StackLayout
            {
                Children =
                {
                    repositories,
                    new Label { Text = "Repositories", Style = (Style)Application.Current.Resources["ProfileStatName"] }
                }
            };

            Label gists = new Label { Style = (Style)Application.Current.Resources["ProfileStatValue"] };
            gists.SetBinding(Label.TextProperty, nameof(ProfileViewModel.Gists));

            StackLayout gistsLayout = new StackLayout
            {
                Children =
                {
                    gists,
                    new Label { Text = "Gists", Style = (Style)Application.Current.Resources["ProfileStatName"] }
                }
            };

            var statsNew = new StackLayout
            {
                //Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                //VerticalOptions = LayoutOptions.FillAndExpand,
                Children = 
                { 
                    followersLayout,
                    followingLayout,
                    repositoriesLayout,
                    gistsLayout
                }
            };

            stats.Children.Add(followersLayout);
            stats.Children.Add(followingLayout, 1, 0);
            stats.Children.Add(repositoriesLayout, 2, 0);
            stats.Children.Add(gistsLayout, 3, 0);

            image.SetBinding(Image.SourceProperty, nameof(ProfileViewModel.AvatarUrl));             

            StackLayout rootLayout = new StackLayout 
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = 
                { 
                    header, 
                    new BoxView { HorizontalOptions = LayoutOptions.FillAndExpand, HeightRequest = 1, Color = Color.FromHex("#E772D3") },
                    statsNew                    
                }            
            };

            Content = rootLayout;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.Initialize().ConfigureAwait(false);
        }
    }
}

