using System.Threading.Tasks;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Services
{
    public class Navigator : INavigator
    {
        public Task GoTo(string route)
        {
            return Shell.Current.GoToAsync(route);
        }

        public Task PopAsync()
        {
            return Shell.Current.Navigation.PopAsync(true);
        }
    }
}


