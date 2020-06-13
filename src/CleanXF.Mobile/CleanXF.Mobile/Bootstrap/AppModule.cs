using Autofac;
using CleanXF.Mobile.Factories;
using CleanXF.Mobile.Services;
using CleanXF.Mobile.ViewModels;
using System.Reflection;
using Xamarin.Forms;

namespace CleanXF.Mobile.Bootstrap
{
    public class AppModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // app instance
            builder.RegisterInstance(Application.Current).SingleInstance();

            builder.RegisterType<Navigator>().As<INavigator>().SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .Where(t => t.Name.EndsWith("Page"));

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
           .Where(t => t.IsSubclassOf(typeof(BaseViewModel)));

            builder.RegisterType<PageFactory>().As<IPageFactory>().SingleInstance();
        }
    }
}
