using Autofac;
using OpenStandup.Core.Interfaces.Services;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Views
{
    public class EditPostPage : ContentPage
    {
        private const int _maxLength = 200;
        private readonly ICameraService _cameraService = App.Container.Resolve<ICameraService>();

        public EditPostPage()
        {
            Title = "Post an Update";

            var button = new Button {Text = "close"};
            button.Clicked +=  async (sender, args) =>
            {
                //ICameraService
                // await _cameraService.TakePhotoAsync();
                await Navigation.PopAsync();
            };

            var editor = new Editor
            {
                MaxLength = _maxLength,
                //VerticalOptions = LayoutOptions.FillAndExpand,
                Placeholder = "What are you working on?",
                HeightRequest = 125
            };

            var label = new Label { Text = $"{_maxLength} characters remaining" };
            var htmlLabel = new Label
            {
                Text = "This is <a href=\"https://github.com/mmacneil/open-standup\">link</a> text.",
                TextType = TextType.Html
            };

            editor.TextChanged += (sender, args) =>
            {
                label.Text = $"{_maxLength - editor.Text.Length} characters remaining";
            };

            var stackLayout = new StackLayout
            {
                Children = { editor, label, htmlLabel, button }
            };

            Content = stackLayout;
        }
    }
}
