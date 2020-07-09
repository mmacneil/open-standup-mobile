using Autofac;
using CleanXF.Mobile.Factories;
using CleanXF.Mobile.Services;
using CleanXF.Mobile.ViewModels;
using ShellLogin;
using System.Reflection;


namespace CleanXF.Mobile.Bootstrap
{
    public class AppModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Navigator>().As<INavigator>().SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .Where(t => t.Name.EndsWith("Page"));

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
           .Where(t => t.IsSubclassOf(typeof(BaseViewModel)));

            builder.RegisterType<PageFactory>().As<IPageFactory>().SingleInstance();

            builder.RegisterType<AppShell>();
        }
    }
}
