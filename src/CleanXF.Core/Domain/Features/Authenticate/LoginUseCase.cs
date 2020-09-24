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
        private readonly IProfileRepository _profileRepository;
        private readonly IGitHubGraphQLApi _gitHubGraphQLApi;

        public LoginUseCase(IAuthenticator authenticator, ISessionRepository sessionRepository, IProfileRepository profileRepository, IGitHubGraphQLApi gitHubGraphQLApi)
        {
            _authenticator = authenticator;
            _sessionRepository = sessionRepository;
            _profileRepository = profileRepository;
            _gitHubGraphQLApi = gitHubGraphQLApi;
        }

        public async Task<bool> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            var authenticationResponse = await _authenticator.Authenticate();
            var result = false;
            AuthenticationResponse useCaseResponse;

            if (authenticationResponse.Succeeded)
            {
                await _sessionRepository.Initialize(authenticationResponse.Payload).ConfigureAwait(false);

                // Fetch and store user's profile info             
                await _profileRepository.Insert(await _gitHubGraphQLApi.GetGitHubViewer().ConfigureAwait(false)).ConfigureAwait(false);
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
