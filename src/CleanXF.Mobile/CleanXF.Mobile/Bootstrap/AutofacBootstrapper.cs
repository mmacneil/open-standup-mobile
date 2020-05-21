using Autofac;
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
            ConfigureApplication(container);
        }

        protected virtual void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<InfrastructureModule>();
        }

        protected abstract void RegisterPages(/*IPageFactory pageFactory*/);

        protected abstract void ConfigureApplication(IContainer container);
    }
}
