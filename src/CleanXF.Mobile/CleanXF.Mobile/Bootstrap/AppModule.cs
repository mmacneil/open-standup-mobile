using Autofac;
using CleanXF.Mobile.Services;
using CleanXF.Mobile.ViewModels;
using System.Reflection;


namespace CleanXF.Mobile.Bootstrap
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
