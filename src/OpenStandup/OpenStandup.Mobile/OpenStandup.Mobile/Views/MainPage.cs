using System;
using Autofac;
using OpenStandup.Mobile.Controls;
using OpenStandup.Mobile.Factories;
using OpenStandup.Mobile.ViewModels;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Views
{
    public class MainPage : ContentPage
    {
        private readonly IPopupNavigation _popupNavigation = App.Container.Resolve<IPopupNavigation>();
        private readonly MainViewModel _viewModel = App.Container.Resolve<MainViewModel>();
        private readonly IPageFactory _pageFactory = App.Container.Resolve<IPageFactory>();

        public MainPage()
        {
            Title = "Recent Posts";
            BindingContext = _viewModel;

            var layout = new AbsoluteLayout();

            var collectionView = new CollectionView();
            collectionView.SetBinding(ItemsView.ItemsSourceProperty, "PostSummaries");

            collectionView.ItemTemplate = new DataTemplate(() =>
            {
                /*Grid grid = new Grid { Padding = 10 };
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                Image image = new Image { Aspect = Aspect.AspectFill, HeightRequest = 60, WidthRequest = 60 };
                image.SetBinding(Image.SourceProperty, "ImageUrl");

                Label nameLabel = new Label { FontAttributes = FontAttributes.Bold };
                nameLabel.SetBinding(Label.TextProperty, "Name");

                Label locationLabel = new Label { FontAttributes = FontAttributes.Italic, VerticalOptions = LayoutOptions.End };
                locationLabel.SetBinding(Label.TextProperty, "Location");

                Grid.SetRowSpan(image, 2);

                grid.Children.Add(image);
                grid.Children.Add(nameLabel, 1, 0);
                grid.Children.Add(locationLabel, 1, 1);

                return grid;*/

                var grid = new Grid { Padding = 10, RowSpacing = 0 };

                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                var avatar = new Image { Aspect = Aspect.AspectFill };
                avatar.SetBinding(Image.SourceProperty, "AvatarUrl");

                var webView = new WebView { HeightRequest = 65 };

                var webViewSource = new HtmlWebViewSource();
                webViewSource.SetBinding(HtmlWebViewSource.HtmlProperty, "HtmlText");

                webView.Source = webViewSource;

                webView.Navigating += async (s, e) =>
                {
                    var uri = new Uri(e.Url);
                    await Launcher.OpenAsync(uri);
                    e.Cancel = true;
                };

                var loginLabel = new Label { Style = (Style)Application.Current.Resources["MetaLabel"] };
                loginLabel.SetBinding(Label.TextProperty, "Login");

                var loginTapGestureRecognizer = new TapGestureRecognizer();
                loginTapGestureRecognizer.Tapped += async (sender, args) =>
                {
                    await _popupNavigation.PushAsync(_pageFactory.Resolve<ProfileViewModel>(vm=>vm.AuthorLogin=loginLabel.Text) as PopupPage);
                };

                loginLabel.GestureRecognizers.Add(loginTapGestureRecognizer);

                var modifiedLabel = new Label { Style = (Style)Application.Current.Resources["MetaLabel"] };
                modifiedLabel.SetBinding(Label.TextProperty, "Modified");
                
                grid.Children.Add(new RoundImage(avatar, 35, 35, 20));
                grid.Children.Add(new StackLayout{Children = { loginLabel, modifiedLabel }, Orientation = StackOrientation.Horizontal}, 1, 0);

                return grid;
            });

            var refreshView = new RefreshView { Content = collectionView };

            AbsoluteLayout.SetLayoutBounds(refreshView, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(refreshView, AbsoluteLayoutFlags.SizeProportional);

            var fab = new ImageButton
            {
                Style = (Style)Application.Current.Resources["FloatingActionButton"],
                Source = "ic_edit.png"
            };

            fab.Clicked += OnImageButtonClicked;

            AbsoluteLayout.SetLayoutBounds(fab, new Rectangle(.95, .95, 55, 55));
            AbsoluteLayout.SetLayoutFlags(fab, AbsoluteLayoutFlags.PositionProportional);

            layout.Children.Add(refreshView);
            layout.Children.Add(fab);

            Content = new StackLayout { Children = { layout } };
        }

        private async void OnImageButtonClicked(object sender, EventArgs e)
        {
            // Page appearance not animated
            await Navigation.PushAsync(new EditPostPage());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.Initialize();
        }
    }
}

