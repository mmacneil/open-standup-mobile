using Android.Widget;
using OpenStandup.Mobile.Droid.Services;
using OpenStandup.Mobile.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(ToastService))]
namespace OpenStandup.Mobile.Droid.Services
{
    public class ToastService : IToastService
    {
        public void Show(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long)?.Show();
        }
    }
}
