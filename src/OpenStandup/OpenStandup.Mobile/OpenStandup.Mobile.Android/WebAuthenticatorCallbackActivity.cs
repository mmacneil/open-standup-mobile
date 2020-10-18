using Android.App;
using Android.Content;
using Android.Content.PM;

namespace OpenStandup.Mobile.Droid
{
    [Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(new[] { Intent.ActionView },
       Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
       DataScheme = "myapp")]
    public class WebAuthenticatorCallbackActivity : Xamarin.Essentials.WebAuthenticatorCallbackActivity
    {
    }
}