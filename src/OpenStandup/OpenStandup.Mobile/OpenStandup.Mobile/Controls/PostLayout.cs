using System;
using System.Threading.Tasks;
using Autofac;
using OpenStandup.Common.Dto;
using OpenStandup.Core.Interfaces;
using OpenStandup.Core.Interfaces.Apis;
using OpenStandup.Mobile.Converters;
using OpenStandup.Mobile.Factories;
using OpenStandup.Mobile.Interfaces;
using OpenStandup.Mobile.ViewModels;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Controls
{
    public class PostLayout : Grid
    {
        private readonly IAppContext _appContext = App.Container.Resolve<IAppContext>();
        private readonly IAppSettings _appSettings = App.Container.Resolve<IAppSettings>();
        private readonly IBaseUrl _baseUrl = App.Container.Resolve<IBaseUrl>();
        private readonly IDialogProvider _dialogProvider = App.Container.Resolve<IDialogProvider>();
        private readonly IOpenStandupApi _openStandupApi = App.Container.Resolve<IOpenStandupApi>();
        private readonly IPageFactory _pageFactory = App.Container.Resolve<IPageFactory>();
        private readonly IPopupNavigation _popupNavigation = App.Container.Resolve<IPopupNavigation>();
        private readonly IToastService _toastService = App.Container.Resolve<IToastService>();


        public PostLayout(PostViewMode viewMode, Func<Task> deleteHandler, bool hasImage = false)
        {
            Padding = new Thickness(0, 10);
            RowSpacing = 0;

            this.SetBinding(AutomationIdProperty, nameof(PostSummaryDto.Id));

            RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            if (hasImage)
            {
                RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }

            RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

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
            loginLabel.SetBinding(AutomationIdProperty, nameof(PostSummaryDto.GitHubId));

            var loginTapGestureRecognizer = new TapGestureRecognizer();
            loginTapGestureRecognizer.Tapped += async (sender, args) =>
            {
                await _popupNavigation.PushAsync(_pageFactory.Resolve<ProfileViewModel>(vm =>
                {
                    vm.SelectedGitHubId = loginLabel.AutomationId;
                    vm.SelectedLogin = loginLabel.Text;
                }) as PopupPage);
            };

            loginLabel.GestureRecognizers.Add(loginTapGestureRecognizer);

            var modifiedLabel = new Label { Style = (Style)Application.Current.Resources["MetaLabel"], VerticalOptions = LayoutOptions.Center };
            modifiedLabel.SetBinding(Label.TextProperty, nameof(PostSummaryDto.Modified));

            Children.Add(new StackLayout { Padding = new Thickness(13, 0), Children = { new RoundImage(avatar, 35, 35, 20), loginLabel, modifiedLabel }, Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.Start });
            Children.Add(webView, 0, 1);

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

                image.SetBinding(Image.SourceProperty, new Binding(nameof(PostSummaryDto.ImageName), BindingMode.Default,
                    new StringToUriImageSourceConverter(),
                    $"{_appSettings.Host}/images/posts/"));

                Children.Add(image, 0, 2);
            }

            var commentLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label { Style = (Style)Application.Current.Resources["MetaIcon"], Text = IconFont.CommentMultiple },
                    new Label { Text = "0", Style = (Style)Application.Current.Resources["MetaText"] }
                }
            };

            if (viewMode == PostViewMode.Summary)
            {
                var commentTapGestureRecognizer = new TapGestureRecognizer();
                commentTapGestureRecognizer.Tapped += async (sender, args) =>
                {
                    await _popupNavigation.PushAsync(_pageFactory.Resolve<PostDetailViewModel>(vm => vm.Id = Convert.ToInt32(AutomationId)) as PopupPage);
                };

                commentLayout.GestureRecognizers.Add(commentTapGestureRecognizer);
            }

            var deleteLayout = new StackLayout
            {
                Children =
                {
                    new Label { Style = (Style)Application.Current.Resources["MetaIcon"], Text = IconFont.Delete },
                    new Label { Text = "delete", Style = (Style)Application.Current.Resources["MetaCommandText"] }
                },
                Style = (Style)Application.Current.Resources["MetaCommandLayout"]
            };

            deleteLayout.SetBinding(IsVisibleProperty, new Binding(nameof(PostSummaryDto.GitHubId), BindingMode.Default, new UserIdIsMeBoolConverter(), _appContext.User.Id));

            var deleteTapGestureRecognizer = new TapGestureRecognizer();
            deleteTapGestureRecognizer.Tapped += async (sender, args) =>
            {
                if (!await _dialogProvider.DisplayAlert("Delete", "Delete this post?", "Yes", "No"))
                {
                    return;
                }

                await _openStandupApi.DeletePost(Convert.ToInt32(AutomationId));
                await deleteHandler();
                _toastService.Show("Post deleted");
            };

            deleteLayout.GestureRecognizers.Add(deleteTapGestureRecognizer);

            Children.Add(new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 15,
                Padding = new Thickness(12, 0, 0, 0),
                Children =
                {
                    commentLayout,
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label { Style = (Style)Application.Current.Resources["MetaIcon"], Text = IconFont.ThumbUp },
                            new Label { Text = "0", Style = (Style)Application.Current.Resources["MetaText"] }
                        }
                    },
                    deleteLayout
                },
            }, 0, hasImage ? 3 : 2);

            Children.Add(new BoxView { Margin = new Thickness(0, 6, 0, 0), HorizontalOptions = LayoutOptions.FillAndExpand, HeightRequest = 1, Color = Color.FromHex("#d9dadc") }, 0, hasImage ? 4 : 3);
        }
    }
}


