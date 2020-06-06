using CleanXF.Core.Domain.Features.Authenticate.Models;
using CleanXF.Core.Interfaces.Authentication;
using CleanXF.Core.Interfaces.Data.Repositories;
using CleanXF.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanXF.Core.Domain.Features.Authenticate
{
    public class LoginUseCase : IRequestHandler<AuthenticationRequest, AuthenticationResponse>
    {
        private readonly IAuthenticator _authenticator;
        private readonly ISessionRepository _sessionRepository;

        public LoginUseCase(IAuthenticator authenticator, ISessionRepository sessionRepository)
        {
            _authenticator = authenticator;
            _sessionRepository = sessionRepository;
        }

        public async Task<AuthenticationResponse> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            var response = await _authenticator.Authenticate();

            if (response.Succeeded)
            {
                await _sessionRepository.Initialize(response.Payload);
                return new AuthenticationResponse(OperationResult.Succeeded, response.Payload);
            }

            // Authentication failed
            return new AuthenticationResponse(response.OperationResult, null, response.ErrorText);
        }
    }
}
