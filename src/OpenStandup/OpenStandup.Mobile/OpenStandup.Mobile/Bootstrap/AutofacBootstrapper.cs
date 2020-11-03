using Autofac;
using OpenStandup.Core;
using OpenStandup.Mobile.Factories;
using OpenStandup.Mobile.Infrastructure;
using System;
using System.Reflection;
using OpenStandup.Mobile.Infrastructure.Mapping;

namespace OpenStandup.Mobile.Bootstrap
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
            builder.RegisterModule<AppModule>();
            builder.RegisterModule<CoreModule>();
            builder.RegisterModule(new InfrastructureModule
            {
                ApplicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
            });
            builder.RegisterModule(new AutoMapperModule(Assembly.GetAssembly(typeof(UserProfile))));
        }

        protected abstract void RegisterPages(IPageFactory pageFactory);

        protected abstract void ConfigureApplication(IContainer container);
    }
}

