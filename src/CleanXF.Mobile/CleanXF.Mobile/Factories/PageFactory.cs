using Autofac;
//using CleanXF.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CleanXF.Mobile.Factories
{
    /*
    public class PageFactory : IPageFactory
    {
        public readonly IDictionary<Type, Type> Map = new Dictionary<Type, Type>();
        private readonly IComponentContext _componentContext;

        public PageFactory(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public void Register<TViewModel, TView>() where TViewModel : BaseViewModel where TView : Page
        {
            Map[typeof(TViewModel)] = typeof(TView);
        }

        public Page Resolve<TViewModel>(Action<TViewModel> setStateAction = null) where TViewModel : BaseViewModel
        {
            return Resolve(out _, setStateAction);
        }

        public Page Resolve<TViewModel>(out TViewModel viewModel, Action<TViewModel> setStateAction = null) where TViewModel : BaseViewModel
        {
            var pageType = Map[typeof(TViewModel)];

            viewModel = _componentContext.Resolve<TViewModel>();

            if (!(_componentContext.Resolve(pageType) is Page page) || viewModel == null)
            {
                throw new InvalidOperationException("View/ViewModel not registered or View Model does not extend BaseViewModel or View is a ContentView instead of ContentPage");
            }

            if (setStateAction != null)
                viewModel.SetState(setStateAction);

            page.BindingContext = viewModel;
            return page;
        }

        public Page Resolve<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel
        {
            var pageType = Map[typeof(TViewModel)];
            if (!(_componentContext.Resolve(pageType) is Page page)) throw new InvalidOperationException("Cannot resolve viewModel");
            page.BindingContext = viewModel;
            return page;
        }
    }*/
}
