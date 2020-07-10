using Autofac;
using CleanXF.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CleanXF.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        internal readonly LoginViewModel _viewModel = App.Container.Resolve<LoginViewModel>();

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = _viewModel;
        }
        protected override bool OnBackButtonPressed() => true;
    }
}