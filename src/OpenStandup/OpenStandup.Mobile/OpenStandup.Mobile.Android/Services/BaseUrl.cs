using OpenStandup.Mobile.Droid.Services;
using OpenStandup.Mobile.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(BaseUrl))]
namespace OpenStandup.Mobile.Droid.Services
{
    public class BaseUrl : IBaseUrl
    {
        public string Get()
        {
            return "file:///android_asset/";
        }
    }
}