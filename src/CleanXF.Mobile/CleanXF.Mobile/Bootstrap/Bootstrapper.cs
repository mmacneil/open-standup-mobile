using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanXF.Mobile.Bootstrap
{
    public class Bootstrapper : AutofacBootstrapper
    {
        protected override void ConfigureApplication(IContainer container)
        {
            /*
            var pageFactory = container.Resolve<IPageFactory>();
            _app.MainPage = pageFactory.Resolve<InitializeViewModel>();
            ContainerProvider.Container = container;*/
        }

        protected override void RegisterPages(/*IPageFactory pageFactory*/)
        {
            throw new NotImplementedException();
        }
    }
}
