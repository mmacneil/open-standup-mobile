using CleanXF.Core.Domain.Features.Authenticate.Models;
using CleanXF.Core.Interfaces.Authentication;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanXF.Core.Domain.Features.Authenticate
{
    public class LoginUseCase : IRequestHandler<AuthenticationRequest, AuthenticationResponse>
    {
        private readonly IAuthenticator _authenticator;

        public LoginUseCase(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        public async Task<AuthenticationResponse> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            await _authenticator.Authenticate();
            return new AuthenticationResponse(true);
        }
    }
}
