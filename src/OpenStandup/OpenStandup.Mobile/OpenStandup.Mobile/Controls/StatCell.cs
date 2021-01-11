using OpenStandup.Mobile.Helpers;
using OpenStandup.Mobile.Models;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Controls
{
    public class StatCell : StackLayout
    {
        public StatCell()
        {
            Padding = new Thickness(8);
            var value = new Label { Style = ResourceDictionaryHelper.GetStyle("ProfileStatValue") };
            value.SetBinding(Label.TextProperty, nameof(StatModel.Value));
            var name = new Label { Style = ResourceDictionaryHelper.GetStyle("ProfileStatName") };
            name.SetBinding(Label.TextProperty, nameof(StatModel.Name));
            Children.Add(value);
            Children.Add(name);
        }
    }
}



