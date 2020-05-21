using CleanXF.Core.Interfaces.Authentication;
using System.Threading.Tasks;

namespace CleanXF.Mobile.ViewModels
{
    public class InitializeViewModel : BaseViewModel
    {
        private readonly IAuthenticator _authenticator;

        public InitializeViewModel(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        public async Task Initialize()
        {
            IsBusy = true;
            await _authenticator.Authenticate();
        }
    }
}
