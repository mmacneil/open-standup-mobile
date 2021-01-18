using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using OpenStandup.Mobile.Controls;
using OpenStandup.Mobile.DataTemplates;
using OpenStandup.Mobile.Helpers;
using OpenStandup.Mobile.ViewModels;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Views
{
    public class MainPage : ContentPage
    {
        private readonly Thickness _postLayoutPadding = new Thickness(0, 10);
        private readonly MainViewModel _viewModel = App.Container.Resolve<MainViewModel>();

        public MainPage()
        {
            Title = "Recent Posts";
            BindingContext = _viewModel;
            var postWithImage = new DataTemplate(() => new PostLayout(PostViewMode.Summary, DeleteHandler, true) { Padding = _postLayoutPadding });
            var postWithoutImage = new DataTemplate(() => new PostLayout(PostViewMode.Summary, DeleteHandler) { Padding = _postLayoutPadding });

            var layout = new AbsoluteLayout();

            var collectionView = new CollectionView
            {
                ItemTemplate = new PostSummaryDataTemplateSelector
                {
                    PostWithImage = postWithImage,
                    PostWithoutImage = postWithoutImage
                }
            };

            collectionView.SetBinding(ItemsView.ItemsSourceProperty, nameof(_viewModel.PostSummaries));

            var refreshView = new RefreshView { Content = collectionView };

            collectionView.RemainingItemsThreshold = 0;
            collectionView.SetBinding(ItemsView.RemainingItemsThresholdProperty, nameof(_viewModel.ItemThreshold));

            collectionView.RemainingItemsThresholdReached += async delegate
            {
                if (_viewModel.IsBusy || refreshView.IsRefreshing)
                {
                    return;
                }

                _viewModel.IsBusy = true;
                refreshView.IsRefreshing = true;
                await _viewModel.LoadPostSummaries();
                refreshView.IsRefreshing = false;
            };

            ICommand refreshCommand = new Command(async () =>
            {
                if (_viewModel.IsBusy)
                {
                    return;
                }

                _viewModel.Reset();
                await _viewModel.LoadPostSummaries();
                refreshView.IsRefreshing = false;
            });

            refreshView.Command = refreshCommand;

            AbsoluteLayout.SetLayoutBounds(refreshView, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(refreshView, AbsoluteLayoutFlags.SizeProportional);

            var fab = new ImageButton
            {
                Style = ResourceDictionaryHelper.GetStyle("FloatingActionButton"),
                Source = "ic_edit.png"
            };

            fab.Clicked += OnImageButtonClicked;

            AbsoluteLayout.SetLayoutBounds(fab, new Rectangle(.95, .95, 55, 55));
            AbsoluteLayout.SetLayoutFlags(fab, AbsoluteLayoutFlags.PositionProportional);

            layout.Children.Add(refreshView);
            layout.Children.Add(fab);

            Content = new StackLayout { Children = { layout } };

            MessagingCenter.Subscribe<PostDetailPage>(this, "OnDelete", async sender =>
            {
                await DeleteHandler().ConfigureAwait(false);
            });
        }

        private async Task DeleteHandler()
        {
            await _viewModel.Initialize().ConfigureAwait(false);
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

