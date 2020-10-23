﻿using OpenStandup.Mobile.Views; 
using System.Windows.Input;
using Xamarin.Forms;

namespace OpenStandup.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();       
            Routing.RegisterRoute("main/login", typeof(LoginPage));
            BindingContext = this;
        }

        public ICommand ExecuteLogout => new Command(async () => await GoToAsync("main/login"));
    }
}