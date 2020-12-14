using System;
using OpenStandup.Mobile.ViewModels;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Factories
{
    public interface IPageFactory
    {
        void Register<TViewModel, TPage>() where TViewModel : BaseViewModel where TPage : Page;
        Page Resolve<TViewModel>(Action<TViewModel> setStateAction = null) where TViewModel : BaseViewModel;
        Page Resolve<TViewModel>(out TViewModel viewModel, Action<TViewModel> setStateAction = null) where TViewModel : BaseViewModel;
    }
}
