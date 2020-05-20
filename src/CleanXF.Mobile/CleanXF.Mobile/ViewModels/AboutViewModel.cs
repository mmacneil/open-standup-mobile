using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CleanXF.Mobile.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Task.Delay(1000) /*Browser.OpenAsync("https://xamarin.com"))*/);
        }

        public ICommand OpenWebCommand { get; }
    }
}