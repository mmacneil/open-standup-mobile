using OpenStandup.Mobile.Controls;
using OpenStandup.Mobile.ViewModels;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Views
{
    public class PostDetailPage : PopupPage
    {
        private PostDetailViewModel _viewModel;
        private PostLayout _postLayout;
        private readonly Frame _frame;

        public PostDetailPage()
        {
            Animation = new ScaleAnimation
            {
                PositionIn = MoveAnimationOptions.Bottom,
                PositionOut = MoveAnimationOptions.Center,
                ScaleIn = 1,
                ScaleOut = .7,
                DurationIn = 300,
                EasingIn = Easing.Linear
            };

            _frame = new Frame
            {
                Margin = new Thickness(15, 0),
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.White,
                Content = new ActivityIndicator { IsRunning = true }
            };

            Content = new ScrollView
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Content = _frame
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _viewModel = (PostDetailViewModel)BindingContext;
            await _viewModel.Initialize();
            _postLayout = new PostLayout(PostViewMode.Detail, !string.IsNullOrEmpty(_viewModel.PostSummary.ImageName))
            {
                BindingContext = _viewModel.PostSummary
            };

            _frame.Content = _postLayout;
        }
    }
}
