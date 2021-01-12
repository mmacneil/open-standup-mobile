using System.Collections.Generic;
using Autofac;
using OpenStandup.Mobile.Converters;
using OpenStandup.Mobile.Helpers;
using OpenStandup.Mobile.Models;
using OpenStandup.Mobile.ViewModels;
using Rg.Plugins.Popup.Contracts;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Controls
{
    public class ProfileLayout : StackLayout
    {
        private readonly IPopupNavigation _popupNavigation = App.Container.Resolve<IPopupNavigation>();
        private readonly FlexLayout _statsLayout;

        public ProfileLayout()
        {
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

            var login = new Label { Style = ResourceDictionaryHelper.GetStyle("Title") };
            login.SetBinding(Label.TextProperty, nameof(ProfileViewModel.Login));

            var location = new Label { Style = ResourceDictionaryHelper.GetStyle("SubTitle") };
            location.SetBinding(Label.TextProperty, nameof(ProfileViewModel.Location));

            var joined = new Label { Style = ResourceDictionaryHelper.GetStyle("MetaLabel") };
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

            var followButton = new ActionButton { Text = "Follow", HorizontalOptions = LayoutOptions.Center };
            followButton.SetBinding(IsVisibleProperty, new Binding(nameof(ProfileViewModel.CanFollow)));
            followButton.Clicked += async (sender, args) =>
            {
                await ((ProfileViewModel)BindingContext).UpdateFollower(true);
            };

            var unFollowButton = new AppButton { Text = "Unfollow", HorizontalOptions = LayoutOptions.Center, Style = ResourceDictionaryHelper.GetStyle("CancelButton") };
            unFollowButton.SetBinding(IsVisibleProperty, new Binding(nameof(ProfileViewModel.IsFollowing)));
            unFollowButton.Clicked += async (sender, args) =>
            {
                await ((ProfileViewModel)BindingContext).UpdateFollower();
            };

            var closeButton = new Button { Text = "Close", HorizontalOptions = LayoutOptions.Center };

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

            Children.Add(defaultLayout);
            Children.Add(profileLayout);
            Children.Add(actionsLayout);
        }

        public void BindStats(IList<StatModel> statModels)
        {
            BindableLayout.SetItemsSource(_statsLayout, statModels); // Do standard property bindings or ObservableCollections work with SetItemSource? Seems the data source must be hydrated before calling this to render the data
        }
    }
}
