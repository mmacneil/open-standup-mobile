using Xamarin.Forms;
using Application = Xamarin.Forms.Application;

namespace OpenStandup.Mobile.Controls
{
    public class NavigationButton : AppButton
    {
        public NavigationButton()
        {
            Style = (Style)Application.Current.Resources["NavigationButton"];
        }
    }
}