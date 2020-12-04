using OpenStandup.Mobile.Views;
using System.Windows.Input;
using Autofac;
using MediatR;
using OpenStandup.Core.Domain.Features.Logout.Models;
using OpenStandup.Mobile.Interfaces;
using Xamarin.Forms;

namespace OpenStandup.Mobile
{
    public partial class AppShell
    {
        private readonly IMediator _mediator = App.Container.Resolve<IMediator>();
        private readonly IIndicatorPageService _indicatorPageService = App.Container.Resolve<IIndicatorPageService>();

        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("main/login", typeof(LoginPage));
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _indicatorPageService.InitIndicatorPage(new IndicatorPage());
        }

        public ICommand ExecuteLogout => new Command(async () =>
        {
            await _mediator.Send(new LogoutRequest());
            await GoToAsync("///main/login?logout=1");
            //await GoToAsync("main/login");
        });
    }
}
