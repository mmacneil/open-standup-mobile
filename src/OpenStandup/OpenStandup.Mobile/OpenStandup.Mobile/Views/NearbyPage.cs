using Autofac;
using OpenStandup.Common.Dto;
using OpenStandup.Mobile.Controls;
using OpenStandup.Mobile.Converters;
using OpenStandup.Mobile.ViewModels;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Views
{
    public class NearbyPage : ContentPage
    {
        private readonly NearbyViewModel _viewModel = App.Container.Resolve<NearbyViewModel>();

        public NearbyPage()
        {
            Title = "Nerd Nearby";

            BindingContext = _viewModel;

            var grid = new Grid { Padding = new Thickness(13, 20) };

            var activityIndicator = new ActivityIndicator { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };
            activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, nameof(NearbyViewModel.IsBusy));

            var collectionView = new CollectionView
            {
                EmptyView = new ContentView
                {
                    Content = new StackLayout
                    {
                        Children =
                        {
                            new Label
                            {
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                Text = "No nerds found in your vicinity.",
                                VerticalOptions = LayoutOptions.CenterAndExpand
                            }
                        }
                    }
                },
                ItemTemplate = new DataTemplate(() =>
                {
                    var layout = new StackLayout { Margin = new Thickness(0, 0, 0, 15), Spacing = 10 };
                    var userMetaLayout = new UserMetaLayout();
                    userMetaLayout.SetBinding(UserMetaLayout.AvatarSourceProperty, nameof(UserNearbyDto.AvatarUrl));
                    userMetaLayout.SetBinding(UserMetaLayout.GitHubIdProperty, nameof(UserNearbyDto.GitHubId));
                    userMetaLayout.SetBinding(UserMetaLayout.LoginProperty, nameof(UserNearbyDto.Login));
                    var label = new Label { Padding = new Thickness(13, 0) };
                    label.SetBinding(Label.TextProperty, nameof(UserNearbyDto.Distance));
                    layout.Children.Add(userMetaLayout);
                    layout.Children.Add(label);
                    return layout;
                })
            };

            collectionView.SetBinding(ItemsView.ItemsSourceProperty, nameof(NearbyViewModel.UsersNearby));
            collectionView.SetBinding(IsVisibleProperty, new Binding(nameof(NearbyViewModel.IsBusy), BindingMode.Default, new BoolInversionConverter()));

            grid.Children.Add(activityIndicator);
            grid.Children.Add(collectionView);
            Content = grid;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.Initialize();
        }
    }
}
