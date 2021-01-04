using Autofac;
using OpenStandup.Mobile.Factories;
using OpenStandup.Mobile.ViewModels;
using OpenStandup.Mobile.Views;


namespace OpenStandup.Mobile.Bootstrap
{
    public class Bootstrapper : AutofacBootstrapper
    {
        protected override void ConfigureApplication(IContainer container)
        {
            App.Container = container;
            RegisterPages(container.Resolve<IPageFactory>());
        }

        protected override void RegisterPages(IPageFactory pageFactory)
        {
            pageFactory.Register<PostDetailViewModel, PostDetailPage>();
            pageFactory.Register<ProfileViewModel, ProfilePage>();
        }
    }
}
