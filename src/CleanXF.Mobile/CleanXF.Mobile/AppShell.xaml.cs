//using CleanXF.Mobile.Views;
using ShellLogin.Views;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace CleanXF.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("registration", typeof(RegistrationPage));
            Routing.RegisterRoute("main/login", typeof(LoginPage));
            BindingContext = this;
        }

        public ICommand ExecuteLogout => new Command(async () => await GoToAsync("main/login"));
    }
}
