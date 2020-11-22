using Android.App;
using Android.OS;


namespace OpenStandup.Mobile.Droid
{
    // https://raulmonteroc.com/xamarin/setup-splash-screen-xamarin-forms/
    [Activity(Label = "Open Standup", Theme = "@style/Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(MainActivity));
        }
    }
}

