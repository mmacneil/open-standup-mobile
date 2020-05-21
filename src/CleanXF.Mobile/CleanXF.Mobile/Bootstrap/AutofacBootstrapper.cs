using Autofac;
using CleanXF.Mobile.Factories;
using CleanXF.Mobile.Infrastructure;


namespace CleanXF.Mobile.Bootstrap
{
    public abstract class AutofacBootstrapper
    {
        public void Run()
        {
            var builder = new ContainerBuilder();
            ConfigureContainer(builder);
            var container = builder.Build();
            var pageFactory = container.Resolve<IPageFactory>();
            RegisterPages(pageFactory);
            ConfigureApplication(container);       
        }

        protected virtual void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<AppModule>();
            builder.RegisterModule<InfrastructureModule>();
        }

        protected abstract void RegisterPages(IPageFactory pageFactory);

        protected abstract void ConfigureApplication(IContainer container);
    }
}
