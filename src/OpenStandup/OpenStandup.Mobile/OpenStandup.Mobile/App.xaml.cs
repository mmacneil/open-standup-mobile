using OpenStandup.Mobile.Bootstrap;
using Autofac;
using OpenStandup.Core.Interfaces.Services;


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
            var appCenter = Container.Resolve<IAppCenterWrapper>();
            appCenter.Start();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
