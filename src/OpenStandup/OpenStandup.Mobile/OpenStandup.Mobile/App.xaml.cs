using OpenStandup.Mobile.Bootstrap;
using Autofac;


namespace OpenStandup.Mobile
{
    public partial class App
    {
        public static IContainer Container { get; set; }

        public App()
        {
            InitializeComponent();
            var bootstrapper = new Bootstrapper();
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
