using OpenStandup.Mobile.Helpers;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Controls
{
    public class MetaItemCountLayout : StackLayout
    {
        private readonly Color _activeColor = ResourceDictionaryHelper.GetColor("MountainMeadow"), _textColor = ResourceDictionaryHelper.GetColor("RollingStone");

        public static BindableProperty ActiveProperty = BindableProperty.Create(
            nameof(Active),
            typeof(bool),
            typeof(MetaItemCountLayout), false,
            BindingMode.Default, null, ActiveChanged);

        public static BindableProperty CountProperty = BindableProperty.Create(
            nameof(Count),
            typeof(int),
            typeof(MetaItemCountLayout), null,
            BindingMode.Default, null, CountChanged);

        private static void ActiveChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is MetaItemCountLayout @this)) return;

            if (newValue is bool active && active)
            {
                @this._iconLabel.TextColor = @this._activeColor;
            }
        }

        private static void CountChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is MetaItemCountLayout @this)) return;

            if (newValue is int source)
            {
                @this._countLabel.Text = source.ToString();
            }
        }

        public bool Active
        {
            get => (bool)GetValue(ActiveProperty);
            set => SetValue(ActiveProperty, value);
        }

        public int Count
        {
            get => (int)GetValue(CountProperty);
            set => SetValue(CountProperty, value);
        }

        private readonly Label
            _countLabel = new Label { Style = ResourceDictionaryHelper.GetStyle("MetaText") },
            _iconLabel = new Label { Style = ResourceDictionaryHelper.GetStyle("MetaIcon") };

        public MetaItemCountLayout(string icon)
        {
            _countLabel.Text = "0";
            Orientation = StackOrientation.Horizontal;
            _iconLabel.Text = icon;
            Children.Add(_iconLabel);
            Children.Add(_countLabel);
        }

        public void Deactivate()
        {
            Active = false;
            _iconLabel.TextColor = _textColor;
            Count--;
            OnPropertyChanged(nameof(Count));
        }

        public void Activate(bool incrementCount = true)
        {
            Active = true;
            _iconLabel.TextColor = _activeColor;

            if (!incrementCount) return;
            Count++;
            OnPropertyChanged(nameof(Count));
        }
    }
}

