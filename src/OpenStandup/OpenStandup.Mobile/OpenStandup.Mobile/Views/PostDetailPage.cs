using System;
using System.Threading.Tasks;
using OpenStandup.Common.Dto;
using OpenStandup.Core.Interfaces;
using OpenStandup.Mobile.Controls;
using OpenStandup.Mobile.Converters;
using OpenStandup.Mobile.Helpers;
using OpenStandup.Mobile.Interfaces;
using OpenStandup.Mobile.ViewModels;
using Rg.Plugins.Popup.Contracts;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Views
{
    public class PostDetailPage : BaseModalPage
    {
        private PostDetailViewModel _viewModel;
        private PostLayout _postLayout;
        private readonly Grid _grid = new Grid { Padding = new Thickness(0, 10, 0, 0) };
        private readonly ActivityIndicator _activityIndicator = new ActivityIndicator { IsRunning = true };

        private readonly IPopupNavigation _popupNavigation;

        public PostDetailPage(IAppContext appContext, IDialogProvider dialogProvider, IPopupNavigation popupNavigation)
        {
            _popupNavigation = popupNavigation;

            _grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            _grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            _grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            _grid.Children.Add(_activityIndicator);

            var postButton = new Button
            {
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button)),
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Text = "Post",
                Visual = new CustomVisual() // A bridging mechanism to allow this specific button to opt-in to the platform renderer
            };

            postButton.SetBinding(IsEnabledProperty, nameof(PostDetailViewModel.CanPost));

            postButton.Clicked += async (sender, args) =>
            {
                await _viewModel.PublishComment();
            };

            var editor = new EnhancedEditor(100, 5, NamedSize.Micro, 50);
            editor.SetBinding(EnhancedEditor.IsValidProperty, nameof(PostDetailViewModel.CanPost));
            editor.SetBinding(EnhancedEditor.TextProperty, nameof(PostDetailViewModel.CommentText));

            _grid.Children.Add(new StackLayout
            {
                Children = {
                    new Label { Style = ResourceDictionaryHelper.GetStyle("MetaCommandText"), Text = "Comment"},
                    editor,
                    postButton
                },
                Padding = new Thickness(8, 0),
                Spacing = 2
            }, 0, 1);

            var commentsView = new CollectionView
            {
                ItemTemplate = new DataTemplate(() =>
                {
                    var grid = new Grid { Margin = new Thickness(0, 0, 0, 10) };
                    grid.SetBinding(AutomationIdProperty, nameof(CommentDto.Id));
                    grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                    var userMetaLayout = new UserMetaLayout(22, 22, 11);
                    userMetaLayout.SetBinding(UserMetaLayout.AvatarSourceProperty, nameof(CommentDto.AvatarUrl));
                    userMetaLayout.SetBinding(UserMetaLayout.GitHubIdProperty, nameof(CommentDto.GitHubId));
                    userMetaLayout.SetBinding(UserMetaLayout.LoginProperty, nameof(CommentDto.Login));
                    userMetaLayout.SetBinding(UserMetaLayout.ModifiedProperty, nameof(CommentDto.Modified));

                    var textLabel = new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                        Margin = new Thickness(15, 5, 15, 5),
                        Style = ResourceDictionaryHelper.GetStyle("ContentLabel"),
                        TextColor = ResourceDictionaryHelper.GetColor("6")
                    };

                    textLabel.SetBinding(Label.TextProperty, nameof(CommentDto.Text));

                    var deleteLayout = new DeleteLayout(async () =>
                    {
                        if (!await dialogProvider.DisplayAlert("Delete", "Delete this comment? ", "Yes", "No"))
                        {
                            return;
                        }

                        await _viewModel.DeleteComment(Convert.ToInt32(grid.AutomationId));
                    })
                    {
                        HorizontalOptions = LayoutOptions.Start,
                        Margin = new Thickness(10, 0, 0, 0)
                    };

                    deleteLayout.SetBinding(IsVisibleProperty, new Binding(nameof(PostDto.GitHubId), BindingMode.Default, new UserIdIsMeBoolConverter(), appContext.User.Id));

                    grid.Children.Add(userMetaLayout);
                    grid.Children.Add(textLabel, 0, 1);
                    grid.Children.Add(deleteLayout, 0, 2);
                    return grid;
                }),
                Margin = new Thickness(0, 10, 0, 0)
            };

            commentsView.SetBinding(ItemsView.ItemsSourceProperty, nameof(_viewModel.Comments));

            var commentsLayout = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Margin = new Thickness(15, 0, 0, 0),
                        Style = ResourceDictionaryHelper.GetStyle("MetaLabel"),
                        Text = "Comments"
                    },
                    new BoxView
                    {
                        Margin = new Thickness(0, 6, 0, 0),
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        HeightRequest = 1,
                        Color = Color.FromHex("#d9dadc")
                    },
                    commentsView
                },
                Spacing = 2
            };

            _grid.Children.Add(commentsLayout, 0, 2);

            Content = new Frame
            {
                Style = ResourceDictionaryHelper.GetStyle("ModalFrame"),
                Content = _grid
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            _viewModel = (PostDetailViewModel)BindingContext;
            await _viewModel.Initialize();

            // Unauthorized presenter support.
            if (_viewModel.Post == null) return;

            _postLayout = new PostLayout(PostViewMode.Detail, DeleteHandler, !string.IsNullOrEmpty(_viewModel.Post.ImageName))
            {
                BindingContext = _viewModel.Post
            };

            _activityIndicator.IsVisible = false;
            _grid.Children.Add(_postLayout);
        }

        private async Task DeleteHandler()
        {
            await _popupNavigation.PopAsync().ConfigureAwait(false);
            MessagingCenter.Send(this, "OnDelete");
        }
    }
}
