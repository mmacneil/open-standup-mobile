using CleanXF.Core.Domain.Features.Signout.Models;
using CleanXF.Mobile.Factories;
using CleanXF.Mobile.ViewModels;
using MediatR;
using System.Windows.Input;
using Xamarin.Forms;

namespace CleanXF.Mobile
{
    public partial class AppShell : Shell
    {
        public ICommand SignoutCommand => new Command(async () =>
        {
            // Call the signout usecase and if successful, navigate back to login
            if (await _mediator.Send(new SignoutRequest()))
            {
                Application.Current.MainPage = _pageFactory.Resolve<LoginViewModel>(vm => { vm.AutoLogin = false; });
            }
        });

        private readonly IMediator _mediator;
        private readonly Application _app;
        private readonly IPageFactory _pageFactory;

        public AppShell(IMediator mediator, Application app, IPageFactory pageFactory)
        {
            InitializeComponent();
            _mediator = mediator;
            _app = app;
            _pageFactory = pageFactory;
            BindingContext = this;
        }
    }
}
