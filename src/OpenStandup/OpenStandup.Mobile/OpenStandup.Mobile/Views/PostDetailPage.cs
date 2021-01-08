using System.Threading.Tasks;
using OpenStandup.Mobile.Controls;
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

            _grid.Children.Add(_activityIndicator);

            _grid.Children.Add(new StackLayout
            {
                Children = {new Label { FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)), Text = "Comment"}, new Entry
                {
                    HeightRequest = 50,
                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry)),
                    Placeholder = "..."
                }}
            }, 0, 2);

            var frame = new Frame
            {
                Style = (Style)Application.Current.Resources["ModalFrame"],
                Content = _grid
            };

            Content = new ScrollView
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Content = frame
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _viewModel = (PostDetailViewModel)BindingContext;
            await _viewModel.Initialize();
            _postLayout = new PostLayout(PostViewMode.Detail, DeleteHandler, !string.IsNullOrEmpty(_viewModel.PostSummary.ImageName))
            {
                BindingContext = _viewModel.PostSummary
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
