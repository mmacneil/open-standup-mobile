using Autofac;
using OpenStandup.Mobile.Controls;
using OpenStandup.Mobile.Converters;
using OpenStandup.Mobile.Helpers;
using OpenStandup.Mobile.ViewModels;
using Rg.Plugins.Popup.Contracts;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.Forms;


namespace OpenStandup.Mobile.Views
{
    public class EditPostPage : ContentPage
    {
        private readonly EditPostViewModel _viewModel = App.Container.Resolve<EditPostViewModel>();
        private readonly EnhancedEditor _editor = new EnhancedEditor();
        private readonly IPopupNavigation _popupNavigation = App.Container.Resolve<IPopupNavigation>();

        public EditPostPage()
        {
            Title = "Post an Update";
            BindingContext = _viewModel;

            _editor.SetBinding(EnhancedEditor.IsValidProperty, nameof(EditPostViewModel.CanPost));
            _editor.SetBinding(EnhancedEditor.TextProperty, nameof(EditPostViewModel.Text));

            var postButton = new ActionButton { Text = "Post", HorizontalOptions = LayoutOptions.EndAndExpand };
            postButton.SetBinding(IsEnabledProperty, nameof(EditPostViewModel.CanPost));
            postButton.Clicked += async (sender, args) =>
            {
                await _viewModel.PublishPost();
            };

            var editorLayout = new AbsoluteLayout { HeightRequest = 135 };
            AbsoluteLayout.SetLayoutBounds(_editor, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(_editor, AbsoluteLayoutFlags.SizeProportional);

            var image = new Image { Aspect = Aspect.AspectFill };
            image.SetBinding(Image.SourceProperty, nameof(EditPostViewModel.PhotoPath));

            var previewImage = new RoundImage(image, 40, 40, 10);
            previewImage.SetBinding(IsVisibleProperty, new Binding(nameof(EditPostViewModel.PhotoPath), BindingMode.Default, new StringToBoolConverter()));

            var photoTapGestureRecognizer = new TapGestureRecognizer();
            photoTapGestureRecognizer.Tapped += async (sender, args) =>
            {
                await _viewModel.DeletePhoto();
            };

            previewImage.GestureRecognizers.Add(photoTapGestureRecognizer);

            AbsoluteLayout.SetLayoutBounds(previewImage, new Rectangle(.99, .65, 40, 40));
            AbsoluteLayout.SetLayoutFlags(previewImage, AbsoluteLayoutFlags.PositionProportional);

            editorLayout.Children.Add(_editor);

            editorLayout.Children.Add(previewImage);

            var cameraIcon = new Label { Style = ResourceDictionaryHelper.GetStyle("CameraIcon") };

            TouchEffect.SetNormalTranslationY(cameraIcon, -22);
            TouchEffect.SetNativeAnimation(cameraIcon, true);

            TouchEffect.SetCommand(cameraIcon, new Command(async () =>
            {
                await _viewModel.TakePhoto();
            }));

            var help = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Text = "Formatting Help",
                FontAttributes = FontAttributes.Bold
            };

            TouchEffect.SetCommand(help, new Command(async () =>
            {
                await _popupNavigation.PushAsync(new FormattingHelpPage());
            }));

            var toolbarLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    help,
                    cameraIcon
                }
            };

            var stackLayout = new StackLayout
            {
                Margin = new Thickness(10, 50),
                Children = {
                    new Label {Text = "What are you working on?", FontSize = Device.GetNamedSize(NamedSize.Title, typeof(Label)),  HorizontalTextAlignment = TextAlignment.Center},
                    editorLayout,
                    toolbarLayout,
                    new StackLayout
                    {
                        Margin = new Thickness(0, 15, 0, 0),
                        Orientation = StackOrientation.Horizontal,
                        Children = { postButton }
                    }
               }
            };

            Content = stackLayout;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _editor.Focus();
        }
    }
}
