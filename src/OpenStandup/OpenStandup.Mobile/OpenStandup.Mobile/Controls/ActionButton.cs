using Xamarin.Forms;
using Application = Xamarin.Forms.Application;

namespace OpenStandup.Mobile.Controls
{
    public class ActionButton : AppButton
    {
        public ActionButton()
        {
            Style = (Style)Application.Current.Resources["ActionButton"];
        }
    }
}