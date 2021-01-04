using OpenStandup.SharedKernel.Utilities;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Controls
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
            var view = (IconItem)bindable;

            if (view == null) return;

            var value = (string)newValue;
            view._text.Text = value;

            // Format email
            TryFormatEmail(view, value);
            // Format url
            TryFormatUrl(view, value);
        }

        private static void TryFormatEmail(IconItem view, string value)
        {
            if (!Validation.IsValidEmail(value)) return;
            FormatLink(view, $"mailto:{value}?subject=Hi from OpenStandup");
        }

        private static void TryFormatUrl(IconItem view, string value)
        {
            if (!Validation.IsValidUrl(value)) return;
            FormatLink(view, value);
        }

        private static void FormatLink(IconItem view, string link)
        {
            view._text.TextDecorations = TextDecorations.Underline;
            view._text.TextColor = Color.Blue;

            view.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => await Launcher.OpenAsync(link))
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
            Children.Add(new Label { Style = (Style)Application.Current.Resources["ItemIcon"], Text = icon });
            Children.Add(_text);
        }
    }
}
