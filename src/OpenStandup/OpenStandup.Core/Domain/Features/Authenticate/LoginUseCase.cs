using OpenStandup.Core.Domain.Features.Authenticate.Models;
using OpenStandup.Core.Interfaces.Authentication;
using OpenStandup.Core.Interfaces.Data.GraphQL;
using OpenStandup.Core.Interfaces.Data.Repositories;
using OpenStandup.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using OpenStandup.Core.Interfaces.Apis;

namespace OpenStandup.Core.Domain.Features.Authenticate
{
    public class LoginUseCase : IRequestHandler<AuthenticationRequest, AuthenticationResponse>
    {
        private readonly IAuthenticator _authenticator;
        private readonly ISecureDataRepository _secureDataRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IGitHubGraphQLApi _gitHubGraphQLApi;
        private readonly IOpenStandupApi _openStandupApi;

        public LoginUseCase(IAuthenticator authenticator, ISecureDataRepository secureDataRepository, IProfileRepository profileRepository, IGitHubGraphQLApi gitHubGraphQLApi, IOpenStandupApi openStandupApi)
        {
            _authenticator = authenticator;
            _secureDataRepository = secureDataRepository;
            _profileRepository = profileRepository;
            _gitHubGraphQLApi = gitHubGraphQLApi;
            _openStandupApi = openStandupApi;
        }

        public async Task<AuthenticationResponse> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            var authenticationResponse = await _authenticator.Authenticate().ConfigureAwait(false);

            AuthenticationResponse useCaseResponse = null;

            if (authenticationResponse.Succeeded)
            {
                await _secureDataRepository.SetPersonalAccessToken(authenticationResponse.Payload).ConfigureAwait(false);
                useCaseResponse = new AuthenticationResponse(OperationResult.Succeeded);
            }
            else
            {
                // Some catastrophic failure during the auth step
                await _secureDataRepository.SetPersonalAccessToken("").ConfigureAwait(false);
                useCaseResponse = new AuthenticationResponse(OperationResult.Failed, authenticationResponse.ErrorText);
            }

            request.OutputPort.Handle(useCaseResponse);
            return useCaseResponse;
        }
    }
}
