using CleanXF.Core.Domain.Features.Authenticate.Models;
using CleanXF.Core.Interfaces.Authentication;
using CleanXF.Core.Interfaces.Data.GraphQL;
using CleanXF.Core.Interfaces.Data.Repositories;
using CleanXF.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanXF.Core.Domain.Features.Authenticate
{
    public class LoginUseCase : IRequestHandler<AuthenticationRequest, bool>
    {
        private readonly IAuthenticator _authenticator;
        private readonly ISessionRepository _sessionRepository;
        private readonly IGitHubGraphQLApi _gitHubGraphQLApi;

        public LoginUseCase(IAuthenticator authenticator, ISessionRepository sessionRepository, IGitHubGraphQLApi gitHubGraphQLApi)
        {
            _authenticator = authenticator;
            _sessionRepository = sessionRepository;
            _gitHubGraphQLApi = gitHubGraphQLApi;
        }

        public async Task<bool> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            var authenticationResponse = await _authenticator.Authenticate();
            var  result = false;
            AuthenticationResponse useCaseResponse;

          if (authenticationResponse.Succeeded)
            {
                await _sessionRepository.Initialize(authenticationResponse.Payload).ConfigureAwait(false);

                var gitHubViewer = await _gitHubGraphQLApi.GetGitHubViewer().ConfigureAwait(false);

                useCaseResponse = new AuthenticationResponse(OperationResult.Succeeded, authenticationResponse.Payload);
                result = true;
            }
            else
            {
                // Authentication failed
                useCaseResponse = new AuthenticationResponse(authenticationResponse.OperationResult, null, authenticationResponse.ErrorText);
            }

            request.OutputPort.Handle(useCaseResponse);
            return result;
        }
    }
}
