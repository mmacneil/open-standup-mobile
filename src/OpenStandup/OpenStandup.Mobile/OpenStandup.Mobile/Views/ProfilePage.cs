using Autofac;
using OpenStandup.Mobile.ViewModels;
using OpenStandup.Mobile.Controls;
using OpenStandup.Mobile.Converters;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;


namespace OpenStandup.Mobile.Views
{
    public class ProfilePage : PopupPage
    {
        private readonly IPopupNavigation _popupNavigation = App.Container.Resolve<IPopupNavigation>();
        private readonly ProfileViewModel _viewModel = App.Container.Resolve<ProfileViewModel>();

        private readonly FlexLayout _statsLayout;

        public ProfilePage()
        {
            BackgroundColor = Color.White;
            Padding = new Thickness(5, 20);

            BindingContext = _viewModel;

            Title = "My Profile";

            var header = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(30, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(70, GridUnitType.Star) }
                }
            };

            var image = new Image
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            var login = new Label { Style = (Style)Application.Current.Resources["Title"] };
            login.SetBinding(Label.TextProperty, nameof(ProfileViewModel.Login));

            var location = new Label { Style = (Style)Application.Current.Resources["SubTitle"] };
            location.SetBinding(Label.TextProperty, nameof(ProfileViewModel.Location));

            var joined = new Label { Style = (Style)Application.Current.Resources["MetaLabel"] };
            joined.SetBinding(Label.TextProperty, nameof(ProfileViewModel.Joined));

            header.Children.Add(new RoundImage(image));
            header.Children.Add(new StackLayout
            {
                Children =
                {
                    login,
                    location,
                    joined
                }
            }, 1, 0);

            _statsLayout = new FlexLayout
            {
                Wrap = FlexWrap.Wrap,
                JustifyContent = FlexJustify.SpaceAround
            };

            BindableLayout.SetItemTemplate(_statsLayout, new DataTemplate(() => new StatCell()));

            image.SetBinding(Image.SourceProperty, nameof(ProfileViewModel.AvatarUrl));

            var email = new IconItem(IconFont.Email);
            email.SetBinding(IsVisibleProperty, new Binding(nameof(ProfileViewModel.Email), BindingMode.Default, new StringToBoolConverter()));
            email.SetBinding(IconItem.TextProperty, new Binding(nameof(ProfileViewModel.Email)));

            var websiteUrl = new IconItem(IconFont.Web);
            websiteUrl.SetBinding(IsVisibleProperty, new Binding(nameof(ProfileViewModel.WebsiteUrl), BindingMode.Default, new StringToBoolConverter()));
            websiteUrl.SetBinding(IconItem.TextProperty, new Binding(nameof(ProfileViewModel.WebsiteUrl)));

            var followButton = new ActionButton { Text = "Follow", HorizontalOptions = LayoutOptions.Center};
            followButton.SetBinding(IsVisibleProperty, new Binding(nameof(ProfileViewModel.CanFollow)));
            followButton.Clicked += async (sender, args) =>
            {
                await _viewModel.UpdateFollower(true);
            };

            var unFollowButton = new AppButton { Text = "Unfollow", HorizontalOptions = LayoutOptions.Center, Style = (Style)Application.Current.Resources["CancelButton"]};
            unFollowButton.SetBinding(IsVisibleProperty, new Binding(nameof(ProfileViewModel.IsFollowing)));
            unFollowButton.Clicked += async (sender, args) =>
            {
                await _viewModel.UpdateFollower();
            };

            var closeButton = new Button { Text = "Close", HorizontalOptions = LayoutOptions.Center};

            closeButton.Clicked += async (sender, args) =>
            {
                await _popupNavigation.PopAsync();
            };

            var actionsLayout = new StackLayout
            {
                Children = { followButton, unFollowButton, closeButton },
                Spacing = 45,
                Margin = new Thickness(0, 35, 0, 0)
            };

            actionsLayout.SetBinding(IsVisibleProperty, new Binding(nameof(ProfileViewModel.ShowActions)));

            var profileLayout = new StackLayout
            {
                Children =
                {
                    header,
                    new BoxView { HorizontalOptions = LayoutOptions.FillAndExpand, HeightRequest = 1, Color = Color.LightGray},
                    _statsLayout,
                    new FlexLayout
                    {
                        Direction = FlexDirection.Column,
                        AlignItems = FlexAlignItems.Center,
                        Padding = new Thickness(0, 25),
                        JustifyContent = FlexJustify.SpaceEvenly,
                        Children =
                        {
                            email,
                            websiteUrl
                        }
                    }
                }
            };

            profileLayout.SetBinding(IsVisibleProperty, new Binding(nameof(ProfileViewModel.Initialized)));

            var activityIndicator = new ActivityIndicator();
            activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, new Binding(nameof(ProfileViewModel.IsBusy)));

            var statusLabel = new Label { Text = "Loading..." };
            statusLabel.SetBinding(Label.TextProperty, new Binding(nameof(ProfileViewModel.StatusText)));

            var defaultLayout = new FlexLayout
            {
                Direction = FlexDirection.Column,
                AlignItems = FlexAlignItems.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                    activityIndicator,
                    statusLabel
                }
            };

            defaultLayout.SetBinding(IsVisibleProperty, new Binding(nameof(ProfileViewModel.Initialized), BindingMode.Default, new BoolInversionConverter()));

            var rootLayout = new StackLayout
            {
                Children =
                {
                    defaultLayout,
                    profileLayout,
                    actionsLayout
                }
            };

            Content = rootLayout;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.Initialize(); //.ConfigureAwait(false);
            BindableLayout.SetItemsSource(_statsLayout, _viewModel.StatModels); // Do standard property bindings or ObservableCollections work with SetItemSource? Seems the data source must be hydrated before calling this to render the data
        }
    }
}

