using Autofac;
using CleanXF.Mobile.Factories;
using CleanXF.Mobile.Services;
using CleanXF.Mobile.ViewModels;
using CleanXF.Mobile.Views;
using System;
using Xamarin.Forms;

namespace CleanXF.Mobile.Bootstrap
{
    public class Bootstrapper : AutofacBootstrapper
    {
        private readonly App _app;

        public Bootstrapper(App app)
        {
            _app = app;
        }

        protected override void ConfigureApplication(IContainer container)
        {           
            DependencyService.Register<MockDataStore>();
            //_app.MainPage = new AppShell();
            var pageFactory = container.Resolve<IPageFactory>();
            _app.MainPage = pageFactory.Resolve<InitializeViewModel>();
        }

        protected override void RegisterPages(IPageFactory pageFactory)
        {
            pageFactory.Register<InitializeViewModel, InitializePage>();
        }
    }
}
