using Autofac;
using CleanXF.Mobile.Factories;
using CleanXF.Mobile.ViewModels;
using MediatR;
using System.Reflection;


namespace CleanXF.Mobile.Bootstrap
{
    public class AppModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .Where(t => t.Name.EndsWith("Page"));

            //builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
           .Where(t => t.IsSubclassOf(typeof(BaseViewModel)));

            builder.RegisterType<PageFactory>().As<IPageFactory>().SingleInstance();
        }
    }
}
