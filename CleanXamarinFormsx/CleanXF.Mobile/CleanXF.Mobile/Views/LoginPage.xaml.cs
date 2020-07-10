using ShellLogin.ViewModels;
using Splat;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShellLogin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPagexx : ContentPage
    {
        public LoginPagexx()
        {
            InitializeComponent();
            BindingContext = ViewModel;
		}

        internal LoginViewModelxx ViewModel { get; set; } = Locator.Current.GetService<LoginViewModelxx>();

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}