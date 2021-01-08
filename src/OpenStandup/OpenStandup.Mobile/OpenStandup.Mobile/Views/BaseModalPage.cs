using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Views
{
    public abstract class BaseModalPage : PopupPage
    {
        protected BaseModalPage()
        {
            Animation = new ScaleAnimation
            {
                PositionIn = MoveAnimationOptions.Bottom,
                PositionOut = MoveAnimationOptions.Center,
                ScaleIn = 1,
                ScaleOut = .7,
                DurationIn = 300,
                EasingIn = Easing.Linear
            };
        }
    }
}
