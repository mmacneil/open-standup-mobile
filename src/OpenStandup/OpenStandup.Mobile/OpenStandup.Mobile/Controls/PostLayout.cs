using System;
using System.Threading.Tasks;
using Autofac;
using OpenStandup.Common.Dto;
using OpenStandup.Core.Interfaces;
using OpenStandup.Core.Interfaces.Apis;
using OpenStandup.Mobile.Converters;
using OpenStandup.Mobile.Factories;
using OpenStandup.Mobile.Helpers;
using OpenStandup.Mobile.Interfaces;
using OpenStandup.Mobile.ViewModels;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using static Xamarin.CommunityToolkit.Effects.TouchEffect;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Controls
{
    public class PostLayout : Grid
    {
        private readonly IAppContext _appContext = App.Container.Resolve<IAppContext>();
        private readonly IAppSettings _appSettings = App.Container.Resolve<IAppSettings>();
        private readonly IDialogProvider _dialogProvider = App.Container.Resolve<IDialogProvider>();
        private readonly IOpenStandupApi _openStandupApi = App.Container.Resolve<IOpenStandupApi>();
        private readonly IPageFactory _pageFactory = App.Container.Resolve<IPageFactory>();
        private readonly IPopupNavigation _popupNavigation = App.Container.Resolve<IPopupNavigation>();
        private readonly IToastService _toastService = App.Container.Resolve<IToastService>();

        public PostLayout(PostViewMode viewMode, Func<Task> deleteHandler, bool hasImage = false)
        {
            RowSpacing = 0;

            this.SetBinding(AutomationIdProperty, nameof(PostDto.Id));

            RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

            var userMetaLayout = new UserMetaLayout();
            userMetaLayout.SetBinding(UserMetaLayout.AvatarSourceProperty, nameof(PostDto.AvatarUrl));
            userMetaLayout.SetBinding(UserMetaLayout.GitHubIdProperty, nameof(PostDto.GitHubId));
            userMetaLayout.SetBinding(UserMetaLayout.LoginProperty, nameof(PostDto.Login));
            userMetaLayout.SetBinding(UserMetaLayout.ModifiedProperty, nameof(PostDto.Modified));

            Children.Add(userMetaLayout);

            var contentLayout = new StackLayout();

            // Alert: Padding borks the TapGestureRecognizer on hyper-linked spans but Margin works
            var textLabel = new Label
            {
                Margin = new Thickness(15, 15, 15, 8),
                Style = ResourceDictionaryHelper.GetStyle("ContentLabel")
            };

            textLabel.SetBinding(Label.FormattedTextProperty, nameof(PostDto.HtmlText), BindingMode.Default, new HtmlLabelConverter());

            contentLayout.Children.Add(textLabel);

            if (hasImage)
            {
                var image = new Image
                {
                    Aspect = Aspect.AspectFill,
                    HeightRequest = 200,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Margin = new Thickness(0, 0, 0, 5)
                };

                image.SetBinding(IsVisibleProperty, nameof(PostDto.ImageName), BindingMode.Default, new StringToBoolConverter());

                image.SetBinding(Image.SourceProperty, new Binding(nameof(PostDto.ImageName), BindingMode.Default,
                    new StringToUriImageSourceConverter(),
                    $"{_appSettings.Host}/images/posts/"));

                contentLayout.Children.Add(image);
            }

            Children.Add(contentLayout, 0, 1);

            var commentLayout = new MetaItemCountLayout(IconFont.CommentMultiple);
            commentLayout.SetBinding(MetaItemCountLayout.CountProperty, nameof(PostDto.CommentCount));

            if (viewMode == PostViewMode.Summary)
            {
                SetNativeAnimation(commentLayout, true);
                SetCommand(commentLayout, new Command(async () =>
                {
                    await _popupNavigation.PushAsync(_pageFactory.Resolve<PostDetailViewModel>(vm => vm.Id = Convert.ToInt32(AutomationId)) as PopupPage);
                }));
            }

            var likeLayout = new MetaItemCountLayout(IconFont.ThumbUp);
            likeLayout.SetBinding(MetaItemCountLayout.ActiveProperty, nameof(PostDto.Liked));
            likeLayout.SetBinding(MetaItemCountLayout.CountProperty, nameof(PostDto.LikeCount));
            SetNativeAnimation(likeLayout, true);
            SetCommand(likeLayout, new Command(async () =>
            {
                if (likeLayout.Active)
                {
                    likeLayout.Deactivate();
                    await _openStandupApi.UnlikePost(Convert.ToInt32(AutomationId));
                }
                else
                {
                    likeLayout.Activate();
                    await _openStandupApi.LikePost(Convert.ToInt32(AutomationId));
                }
            }));

            var deleteLayout = new DeleteLayout(async () =>
            {
                if (!await _dialogProvider.DisplayAlert("Delete", "Delete this post?", "Yes", "No"))
                {
                    return;
                }

                await _openStandupApi.DeletePost(Convert.ToInt32(AutomationId));
                await deleteHandler();
                _toastService.Show("Post deleted");
            });

            deleteLayout.SetBinding(IsVisibleProperty, new Binding(nameof(PostDto.GitHubId), BindingMode.Default, new UserIdIsMeBoolConverter(), _appContext.User.Id));

            Children.Add(new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 15,
                Padding = new Thickness(15, 0, 0, 0),
                Children =
                {
                    commentLayout,
                    likeLayout,
                    deleteLayout
                },
            }, 0, 2);

            Children.Add(new BoxView
            {
                Margin = new Thickness(0, 6, 0, 0),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 1,
                Color = Color.FromHex("#d9dadc")
            }, 0, 3);
        }
    }
}


