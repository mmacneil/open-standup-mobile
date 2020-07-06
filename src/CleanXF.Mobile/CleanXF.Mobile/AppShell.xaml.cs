using System.Windows.Input;
using Xamarin.Forms;

namespace CleanXF.Mobile
{
    public partial class AppShell : Shell
    {
        public ICommand HelpCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));

        public AppShell()
        {
            InitializeComponent();
        }
    }
}
