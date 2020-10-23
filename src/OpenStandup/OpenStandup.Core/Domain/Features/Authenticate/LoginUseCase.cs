using OpenStandup.Core.Domain.Features.Authenticate.Models;
using OpenStandup.Core.Interfaces.Authentication;
using OpenStandup.Core.Interfaces.Data.Repositories;
using OpenStandup.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace OpenStandup.Core.Domain.Features.Authenticate
{
    public class LoginUseCase : IRequestHandler<AuthenticationRequest, AuthenticationResponse>
    {
        private readonly IAuthenticator _authenticator;
        private readonly ISecureDataRepository _secureDataRepository;


        public LoginUseCase(IAuthenticator authenticator, ISecureDataRepository secureDataRepository)
        {
            _authenticator = authenticator;
            _secureDataRepository = secureDataRepository;
        }

        public async Task<AuthenticationResponse> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            var authenticationResponse = await _authenticator.Authenticate().ConfigureAwait(false);

            AuthenticationResponse useCaseResponse;

            if (authenticationResponse.Succeeded)
            {
                await _secureDataRepository.SetPersonalAccessToken(authenticationResponse.Payload).ConfigureAwait(false);
                useCaseResponse = new AuthenticationResponse(OperationResult.Succeeded);
            }
            else
            {
                await _secureDataRepository.SetPersonalAccessToken("").ConfigureAwait(false);
                useCaseResponse = new AuthenticationResponse(OperationResult.Failed, authenticationResponse.ErrorText);
            }

            request.OutputPort.Handle(useCaseResponse);
            return useCaseResponse;
        }
    }
}
