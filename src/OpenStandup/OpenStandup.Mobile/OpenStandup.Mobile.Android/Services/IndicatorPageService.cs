using Android.App;
using Android.Views;
using Android.Graphics.Drawables;
using OpenStandup.Mobile.Droid.Services;
using OpenStandup.Mobile.Interfaces;
using OpenStandup.Mobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

// https://theconfuzedsourcecode.wordpress.com/2018/03/19/build-yo-own-awesome-activity-loading-indicator-page-for-xamarin-forms/
[assembly: Dependency(typeof(IndicatorPageService))]
namespace OpenStandup.Mobile.Droid.Services
{
    public class IndicatorPageService : IIndicatorPageService
    {
        private Android.Views.View _nativeView;

        private Dialog _dialog;

        private bool _isInitialized;

        public void InitIndicatorPage(ContentPage indicatorPage)
        {
            // check if the page parameter is available
            if (indicatorPage != null)
            {
                // build the loading page with native base
                indicatorPage.Parent = Xamarin.Forms.Application.Current.MainPage;

                indicatorPage.Layout(new Rectangle(0, 0,
                    Xamarin.Forms.Application.Current.MainPage.Width,
                    Xamarin.Forms.Application.Current.MainPage.Height));

                var renderer = Platform.CreateRendererWithContext(indicatorPage, Android.App.Application.Context);

                _nativeView = renderer.View;

                _dialog = new Dialog(Xamarin.Essentials.Platform.CurrentActivity);
                _dialog.RequestWindowFeature((int)WindowFeatures.NoTitle);
                _dialog.SetCancelable(false);
                _dialog.SetContentView(_nativeView);
                var window = _dialog.Window;
                window?.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                window?.ClearFlags(WindowManagerFlags.DimBehind);
                window?.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Transparent));

                _isInitialized = true;
            }
        }

        public void ShowIndicatorPage()
        {
            // check if the user has set the page or not
            if (!_isInitialized)
                InitIndicatorPage(new IndicatorPage()); // set the default page

            // showing the native loading page
            _dialog.Show();
        }

        public void HideIndicatorPage()
        {
            // Hide the page
            _dialog?.Dismiss();
            _dialog?.Hide();
        }
    }
}