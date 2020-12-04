using System;
using Autofac;
using OpenStandup.Mobile.Services;
using OpenStandup.Mobile.ViewModels;
using System.Reflection;
using OpenStandup.Mobile.Interfaces;
using Xamarin.Forms;


namespace OpenStandup.Mobile.Bootstrap
{
    public class AppModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
           .Where(t => t.IsSubclassOf(typeof(BaseViewModel))).SingleInstance();

            builder.RegisterType<Navigator>().As<INavigator>().SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(t => t.Name.EndsWith("Presenter"))
                .AsImplementedInterfaces().SingleInstance();

            // Current PageProxy
            builder.RegisterType<PageProxy>().As<IDialogProvider>().SingleInstance();

            builder.RegisterInstance<Func<Page>>(() =>
            {
                var currentPage = Application.Current.MainPage;
                return currentPage.Navigation.ModalStack.Count == 1 ? currentPage.Navigation.ModalStack[0] : currentPage;
            });

            builder.RegisterInstance(GetPlatformDependency<IIndicatorPageService>()).As<IIndicatorPageService>().SingleInstance();
        }

        public static T GetPlatformDependency<T>() where T : class
        {
            var dependency = DependencyService.Get<T>();
            if (dependency == null)
            {
                throw new InvalidOperationException($"Missing '{typeof(T).FullName}' implementation! Implementation is required.");
            }
            return dependency;
        }
    }
}

