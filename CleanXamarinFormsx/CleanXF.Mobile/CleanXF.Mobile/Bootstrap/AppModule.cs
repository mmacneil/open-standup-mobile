using Autofac;
using CleanXF.Mobile.Factories;
using CleanXF.Mobile.Services;
using ShellLogin.Services.Identity;
using ShellLogin.Services.Routing;
using ShellLogin.ViewModels;
//using CleanXF.Mobile.Services;
//using CleanXF.Mobile.ViewModels;
using System.Reflection;


namespace CleanXF.Mobile.Bootstrap
{
    public class AppModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
           .Where(t => t.IsSubclassOf(typeof(BaseViewModel)));

            builder.RegisterType<ShellRoutingService>().As<IRoutingService>().SingleInstance();
            builder.RegisterType<IdentityServiceStub>().As<IIdentityService>().SingleInstance();

            builder.RegisterType<Navigator>().As<INavigator>().SingleInstance();

            /* Locator.CurrentMutable.RegisterLazySingleton<IRoutingService>(() => new ShellRoutingService());
            Locator.CurrentMutable.RegisterLazySingleton<IIdentityService>(() => new IdentityServiceStub());*/

            /*
            builder.RegisterType<Navigator>().As<INavigator>().SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .Where(t => t.Name.EndsWith("Page"));

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
           .Where(t => t.IsSubclassOf(typeof(BaseViewModel)));

            builder.RegisterType<PageFactory>().As<IPageFactory>().SingleInstance();*/

            //builder.RegisterType<AppShell>();
        }
    }
}
