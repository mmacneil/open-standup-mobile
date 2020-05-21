using Xamarin.Forms;
using CleanXF.Mobile.Bootstrap;

namespace CleanXF.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var bootstrapper = new Bootstrapper(this);
            bootstrapper.Run();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
