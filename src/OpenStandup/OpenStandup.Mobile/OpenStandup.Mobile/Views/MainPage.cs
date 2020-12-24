using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using OpenStandup.Common.Dto;
using OpenStandup.Core.Interfaces;
using OpenStandup.Mobile.Controls;
using OpenStandup.Mobile.Converters;
using OpenStandup.Mobile.DataTemplates;
using OpenStandup.Mobile.Factories;
using OpenStandup.Mobile.Interfaces;
using OpenStandup.Mobile.ViewModels;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Views
{
    public class MainPage : ContentPage
    {
        private readonly IAppSettings _appSettings = App.Container.Resolve<IAppSettings>();
        private readonly IPopupNavigation _popupNavigation = App.Container.Resolve<IPopupNavigation>();
        private readonly MainViewModel _viewModel = App.Container.Resolve<MainViewModel>();
        private readonly IPageFactory _pageFactory = App.Container.Resolve<IPageFactory>();
        private readonly IBaseUrl _baseUrl = App.Container.Resolve<IBaseUrl>();

        private DataTemplate _postWithImage;
        private DataTemplate _postWithoutImage;

        public MainPage()
        {
            Title = "Recent Posts";
            BindingContext = _viewModel;
            SetupDataTemplates();

            var layout = new AbsoluteLayout();

            var collectionView = new CollectionView
            {
                ItemTemplate = new PostSummaryDataTemplateSelector
                {
                    PostWithImage = _postWithImage,
                    PostWithoutImage = _postWithoutImage
                }
            };

            collectionView.SetBinding(ItemsView.ItemsSourceProperty, nameof(_viewModel.PostSummaries));

            var refreshView = new RefreshView { Content = collectionView };

            ICommand refreshCommand = new Command(async () =>
            {
                await _viewModel.LoadPostSummaries();
                refreshView.IsRefreshing = false;
            });

            refreshView.Command = refreshCommand;

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

        private void SetupDataTemplates()
        {
            _postWithImage = MakeDataTemplate(true);
            _postWithoutImage = MakeDataTemplate();
        }

        private DataTemplate MakeDataTemplate(bool hasImage = false)
        {
            return new DataTemplate(() =>
            {
                var grid = new Grid { Padding = new Thickness(0, 10), RowSpacing = 0 };

                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                if (hasImage)
                {
                    grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                }

                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                var avatar = new Image { Aspect = Aspect.AspectFill };
                avatar.SetBinding(Image.SourceProperty, nameof(PostSummaryDto.AvatarUrl));

                var webViewSource = new HtmlWebViewSource { BaseUrl = _baseUrl.Get() };
                webViewSource.SetBinding(HtmlWebViewSource.HtmlProperty, nameof(PostSummaryDto.HtmlText));

                var webView = new WebView { Margin = new Thickness(0, 5), Source = webViewSource };

                webView.Navigating += async (s, e) =>
                {
                    var uri = new Uri(e.Url);
                    await Launcher.OpenAsync(uri);
                    e.Cancel = true;
                };

                var loginLabel = new Label { Style = (Style)Application.Current.Resources["LinkLabel"], VerticalOptions = LayoutOptions.Center };
                loginLabel.SetBinding(Label.TextProperty, nameof(PostSummaryDto.Login));

                var loginTapGestureRecognizer = new TapGestureRecognizer();
                loginTapGestureRecognizer.Tapped += async (sender, args) =>
                {
                    await _popupNavigation.PushAsync(_pageFactory.Resolve<ProfileViewModel>(vm =>
                    {
                        vm.SelectedGitHubId = _viewModel.PostSummaries.First(x => x.Login == loginLabel.Text).GitHubId;
                        vm.SelectedLogin = loginLabel.Text;
                    }) as PopupPage);
                };

                loginLabel.GestureRecognizers.Add(loginTapGestureRecognizer);

                var modifiedLabel = new Label { Style = (Style)Application.Current.Resources["MetaLabel"], VerticalOptions = LayoutOptions.Center };
                modifiedLabel.SetBinding(Label.TextProperty, nameof(PostSummaryDto.Modified));

                grid.Children.Add(new StackLayout { Padding = new Thickness(13, 0), Children = { new RoundImage(avatar, 35, 35, 20), loginLabel, modifiedLabel }, Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.Start });
                grid.Children.Add(webView, 0, 1);

                if (hasImage)
                {
                    var image = new Image
                    {
                        Aspect = Aspect.AspectFill,
                        HeightRequest = 200,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Margin = new Thickness(0, 0, 0, 5)
                    };

                    image.SetBinding(IsVisibleProperty, nameof(PostSummaryDto.ImageName), BindingMode.Default, new StringToBoolConverter());

                    var binding = new Binding(nameof(PostSummaryDto.ImageName), BindingMode.Default,
                        new StringToUriImageSourceConverter(),
                        $"{_appSettings.Host}/images/posts/");
                    image.SetBinding(Image.SourceProperty, binding);

                    grid.Children.Add(image, 0, 2);
                }

                grid.Children.Add(new BoxView { Margin = new Thickness(0,12, 0, 0), HorizontalOptions = LayoutOptions.FillAndExpand, HeightRequest = 1, Color = Color.FromHex("#d9dadc") }, 0, hasImage ? 3 : 2);

                return grid;
            });
        }

        private async void OnImageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditPostPage());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.Initialize();
        }
    }
}

