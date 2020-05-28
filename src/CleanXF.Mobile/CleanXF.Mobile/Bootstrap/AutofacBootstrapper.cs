using Autofac;
using CleanXF.Core;
using CleanXF.Mobile.Factories;
using CleanXF.Mobile.Infrastructure;
using System;

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
            builder.RegisterModule<CoreModule>();
            builder.RegisterModule(new InfrastructureModule
            {
                ApplicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
            });
        }

        protected abstract void RegisterPages(IPageFactory pageFactory);

        protected abstract void ConfigureApplication(IContainer container);
    }
}

