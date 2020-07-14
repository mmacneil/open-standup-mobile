using Autofac;
using CleanXF.Mobile;
using ShellLogin.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShellLogin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPage : ContentPage
    {
        public LoadingPage()
        {
            InitializeComponent();
        }

        //internal LoadingViewModel ViewModel { get; set; } = Locator.Current.GetService<LoadingViewModel>();
        private readonly LoadingViewModel ViewModel = App.Container.Resolve<LoadingViewModel>();

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.Init();
		}
    }
}