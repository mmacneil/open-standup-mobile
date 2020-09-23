using CleanXF.SharedKernel.Utilities;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CleanXF.Mobile.Controls
{
    public class IconItem : StackLayout
    {
        public static BindableProperty TextProperty = BindableProperty.Create(
            "Text",
            typeof(string),
            typeof(IconItem),
            string.Empty,
            BindingMode.OneWay,
            propertyChanged: HandleTextPropertyChanged);

        private static void HandleTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var targetView = (IconItem)bindable;

            if (targetView == null) return;

            var value = (string)newValue;
            targetView._text.Text = value;


            if (!Url.IsValidUrl(value)) return;

            // Convert to hyperlink if handling a url
            targetView._text.TextDecorations = TextDecorations.Underline;
            targetView._text.TextColor = Color.Blue;

            targetView._text.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => await Launcher.OpenAsync(value))
            });
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set
            {
                if (Text != value)
                    SetValue(TextProperty, value);
            }
        }

        private readonly Label _text = new Label();

        public IconItem(string icon)
        {
            Padding = new Thickness(0, 10);
            Orientation = StackOrientation.Horizontal;
            Children.Add(new Label { Style = (Style)Application.Current.Resources["MaterialIcon"], Text = icon });
            Children.Add(_text);
        }
    }
}
