using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShellLogin.Views;
using ShellLogin.Services.Routing;
using Splat;
using ShellLogin.Services.Identity;
using ShellLogin.ViewModels;
using CleanXF.Mobile.Bootstrap;
using Autofac;

namespace CleanXF.Mobile
{
    public partial class App : Application
    {
        public static IContainer Container { get; set; }

        public App()
        {
            //InitializeDi();
            InitializeComponent();

            var bootstrapper = new Bootstrapper(this);
            bootstrapper.Run();
            MainPage = new AppShell();
        }

        private void InitializeDi()
        {
            // Services
            Locator.CurrentMutable.RegisterLazySingleton<IRoutingService>(() => new ShellRoutingService());
            Locator.CurrentMutable.RegisterLazySingleton<IIdentityService>(() => new IdentityServiceStub());

            // ViewModels
            Locator.CurrentMutable.Register(() => new LoadingViewModel());
            Locator.CurrentMutable.Register(() => new LoginViewModelxx());
            Locator.CurrentMutable.Register(() => new RegistrationViewModel());
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
