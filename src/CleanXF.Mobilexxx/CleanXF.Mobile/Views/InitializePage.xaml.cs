using Autofac;
using CleanXF.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CleanXF.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InitializePage : ContentPage
    {
        private readonly InitializeViewModel _viewModel = App.Container.Resolve<InitializeViewModel>();

        public InitializePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
             _viewModel.Initialize();
        }
    }
}