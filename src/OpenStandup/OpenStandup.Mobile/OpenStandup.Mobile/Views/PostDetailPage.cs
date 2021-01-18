using System.Threading.Tasks;
using OpenStandup.Common.Dto;
using OpenStandup.Mobile.Controls;
using OpenStandup.Mobile.Helpers;
using OpenStandup.Mobile.ViewModels;
using Rg.Plugins.Popup.Contracts;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Views
{
    public class PostDetailPage : BaseModalPage
    {
        private PostDetailViewModel _viewModel;
        private PostLayout _postLayout;
        private readonly Grid _grid = new Grid();
        private readonly ActivityIndicator _activityIndicator = new ActivityIndicator { IsRunning = true };

        private readonly IPopupNavigation _popupNavigation;

        public PostDetailPage(IPopupNavigation popupNavigation)
        {
            _popupNavigation = popupNavigation;

            _grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
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

            var editor = new EnhancedEditor(100, 5, NamedSize.Micro)
            {
                HeightRequest = 65
            };

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
            }, 0, 2);

            var commentsView = new CollectionView
            {
                ItemTemplate = new DataTemplate(() =>
                {
                    var grid = new Grid();
                    grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                    var authorLayout = new AuthorLayout();
                    authorLayout.SetBinding(AuthorLayout.AvatarSourceProperty, nameof(CommentDto.AvatarUrl));
                    authorLayout.SetBinding(AuthorLayout.GitHubIdProperty, nameof(CommentDto.GitHubId));
                    authorLayout.SetBinding(AuthorLayout.LoginProperty, nameof(CommentDto.Login));
                    authorLayout.SetBinding(AuthorLayout.ModifiedProperty, nameof(CommentDto.Modified));
                    grid.Children.Add(authorLayout);

                    var textLabel = new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                        Margin = new Thickness(15, 0, 15, 8),
                        Style = ResourceDictionaryHelper.GetStyle("ContentLabel"),
                        TextColor = ResourceDictionaryHelper.GetColor("PostBody")
                };

                    textLabel.SetBinding(Label.TextProperty, nameof(CommentDto.Text));
                    grid.Children.Add(textLabel, 0, 1);
                    return grid;
                })
            };

            commentsView.SetBinding(ItemsView.ItemsSourceProperty, nameof(_viewModel.Comments));

            _grid.Children.Add(commentsView, 0, 3);

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
            _postLayout = new PostLayout(PostViewMode.Detail, DeleteHandler, !string.IsNullOrEmpty(_viewModel.Post.ImageName))
            {
                BindingContext = _viewModel.Post
            };

            _activityIndicator.IsVisible = false;
            _grid.Children.Add(_postLayout, 0, 1);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _grid.Children.Remove(_postLayout);
        }

        private async Task DeleteHandler()
        {
            await _popupNavigation.PopAsync().ConfigureAwait(false);
            MessagingCenter.Send(this, "OnDelete");
        }
    }
}
