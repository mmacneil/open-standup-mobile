using CleanXF.Mobile.Models;
using Xamarin.Forms;

namespace CleanXF.Mobile.Controls
{
    public class StatCell : StackLayout
    {
        public StatCell()
        {
            Padding = new Thickness(10);
            var value = new Label { Style = (Style)Application.Current.Resources["ProfileStatValue"] };
            value.SetBinding(Label.TextProperty, nameof(StatModel.Value));
            var name = new Label { Style = (Style)Application.Current.Resources["ProfileStatName"] };
            name.SetBinding(Label.TextProperty, nameof(StatModel.Name));
            Children.Add(value);
            Children.Add(name);
        }
    }
}



