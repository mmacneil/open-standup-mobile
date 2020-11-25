using System.Threading.Tasks;
using Autofac;
using OpenStandup.Mobile.Controls;
using OpenStandup.Mobile.Converters;
using OpenStandup.Mobile.ViewModels;
using Xamarin.Forms;
using Application = Xamarin.Forms.Application;
using Button = Xamarin.Forms.Button;

namespace OpenStandup.Mobile.Views
{
    public class EditPostPage : ContentPage
    {
        private readonly EditPostViewModel _viewModel = App.Container.Resolve<EditPostViewModel>();

        private const int MaxLength = 200;

        private readonly Editor _editor;

        public EditPostPage()
        {
            Title = "Post an Update";
            BindingContext = _viewModel;

            var postButton = new ActionButton { Text = "Post", HorizontalOptions = LayoutOptions.End };

            _editor = new Editor
            {
                MaxLength = MaxLength,
                Placeholder = "..."
            };

            postButton.SetBinding(IsEnabledProperty, nameof(EditPostViewModel.CanPost));

            postButton.Clicked += async (sender, args) =>
            {
                await _viewModel.PublishPost();
            };

            _editor.SetBinding(Editor.TextProperty, nameof(EditPostViewModel.Text));

            var charactersLabel = new Label { Text = $"{MaxLength} characters remaining", FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)) };

            _editor.TextChanged += (sender, args) =>
            {
                charactersLabel.Text = $"{MaxLength - _editor.Text.Length} characters remaining";
            };

            var metaLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Children = { charactersLabel, new Label { FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)), Text = "Repo names will be automatically hyper-linked" } }
            };

            var editorLayout = new AbsoluteLayout { HeightRequest = 170 };
            AbsoluteLayout.SetLayoutBounds(_editor, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(_editor, AbsoluteLayoutFlags.SizeProportional);

            var image = new Image { Aspect = Aspect.AspectFill };
            image.SetBinding(Image.SourceProperty, nameof(EditPostViewModel.PhotoPath));

            var previewImage = new RoundImage(image, 40, 40, 10);
            previewImage.SetBinding(IsVisibleProperty, new Binding(nameof(EditPostViewModel.PhotoPath), BindingMode.Default, new StringToBoolConverter()));

            var photoTapGestureRecognizer = new TapGestureRecognizer();
            photoTapGestureRecognizer.Tapped += async (sender,args) =>
            {
                await _viewModel.DeletePhoto();
            };

            previewImage.GestureRecognizers.Add(photoTapGestureRecognizer);

            AbsoluteLayout.SetLayoutBounds(previewImage, new Rectangle(.99, .99, 55, 55));
            AbsoluteLayout.SetLayoutFlags(previewImage, AbsoluteLayoutFlags.PositionProportional);

            editorLayout.Children.Add(_editor);
            editorLayout.Children.Add(previewImage);

            var cancelButton = new Button
            {
                Text = "Cancel",
                HorizontalOptions = LayoutOptions.EndAndExpand
            };

            cancelButton.Clicked += async (s, a) =>
            {
                await Navigation.PopAsync();
            };

            var buttonsLayout = new StackLayout
            {
                Margin = new Thickness(0, 15, 0, 0),
                Orientation = StackOrientation.Horizontal,
                Children = { cancelButton, postButton }
            };

            var cameraIcon = new Label { Style = (Style)Application.Current.Resources["MaterialIcon"], Text = IconFont.Camera, FontSize = 40 };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (s, e) =>
            {
                await _viewModel.TakePhoto();
            };

            cameraIcon.GestureRecognizers.Add(tapGestureRecognizer);

            var toolbarLayout = new StackLayout
            {
                Margin = new Thickness(12, 0),
                Orientation = StackOrientation.Horizontal,
                Children = { metaLayout, cameraIcon }
            };

            var stackLayout = new StackLayout
            {
                Margin = new Thickness(10, 50),
                Children = {                    
                    new Label {Text = "What are you working on?", FontSize = Device.GetNamedSize(NamedSize.Title, typeof(Label)),  HorizontalTextAlignment = TextAlignment.Center},
                    editorLayout,
                    toolbarLayout,
                    buttonsLayout
               }
            };

            Content = stackLayout;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(100); // ugh: https://forums.xamarin.com/discussion/100354/entry-focus-not-working-for-android
            _editor.Focus();
            await _viewModel.Initialize();
        }
    }
}
