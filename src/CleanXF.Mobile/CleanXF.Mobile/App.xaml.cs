using Xamarin.Forms;
using CleanXF.Mobile.Bootstrap;
using Autofac;

namespace CleanXF.Mobile
{
    public partial class App : Application
    {
        public static IContainer Container { get; set; }

        public App()
        {
            InitializeComponent();
            var bootstrapper = new Bootstrapper(this);
            bootstrapper.Run();
            MainPage = new AppShell();
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
