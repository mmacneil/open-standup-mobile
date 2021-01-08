using System.Threading.Tasks;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Controls
{
    public class EnhancedEditor : StackLayout
    {
        public static BindableProperty IsValidProperty = BindableProperty.Create(
            nameof(IsValid),
            typeof(bool),
            typeof(EnhancedEditor),
            false,
            BindingMode.TwoWay);

        public static BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(EnhancedEditor),
            "",
            BindingMode.TwoWay);

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private const string CharsRemaining = "characters remaining";
        private readonly Editor _editor;

        public EnhancedEditor(int maxLength = 200, int minLength = 5)
        {
            _editor = new Editor
            {
                MaxLength = maxLength,
                Placeholder = "...",
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var charactersLabel = new Label { Text = $"{maxLength} {CharsRemaining}", FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)) };

            _editor.TextChanged += (sender, args) =>
            {
                Text = _editor.Text;
                charactersLabel.Text = $"{maxLength - Text.Length} {CharsRemaining}";
                IsValid = Text.Length >= minLength;
            };

            Children.Add(_editor);
            Children.Add(charactersLabel);
        }

        public new async Task Focus()
        {
            await Task.Delay(100); // ugh: https://forums.xamarin.com/discussion/100354/entry-focus-not-working-for-android
            _editor.Focus();
        }
    }
}
