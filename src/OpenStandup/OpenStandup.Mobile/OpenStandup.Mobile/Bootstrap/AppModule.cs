using Autofac;
using OpenStandup.Mobile.Services;
using OpenStandup.Mobile.ViewModels;
using System.Reflection;


namespace OpenStandup.Mobile.Bootstrap
{
    public class AppModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
           .Where(t => t.IsSubclassOf(typeof(BaseViewModel)));

            builder.RegisterType<Navigator>().As<INavigator>().SingleInstance();
        }
    }
}
