using OpenStandup.Mobile.Views; 
using System.Windows.Input;
using Autofac;
using MediatR;
using OpenStandup.Core.Domain.Features.Signout.Models;
using Xamarin.Forms;

namespace OpenStandup.Mobile
{
    public partial class AppShell : Shell
    {
        private readonly IMediator _mediator = App.Container.Resolve<IMediator>();

        public AppShell()
        {
            InitializeComponent();       
            Routing.RegisterRoute("main/login", typeof(LoginPage));
            BindingContext = this;
        }

        public ICommand ExecuteLogout => new Command(async () =>
        {
            await _mediator.Send(new SignoutRequest());
            await GoToAsync("///main/login?logout=1");
            //await GoToAsync("main/login");
        });
    }
}
