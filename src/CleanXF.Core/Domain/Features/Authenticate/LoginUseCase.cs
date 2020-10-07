using CleanXF.Core.Domain.Features.Authenticate.Models;
using CleanXF.Core.Interfaces.Authentication;
using CleanXF.Core.Interfaces.Data.GraphQL;
using CleanXF.Core.Interfaces.Data.Repositories;
using CleanXF.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CleanXF.Core.Interfaces.Apis;

namespace CleanXF.Core.Domain.Features.Authenticate
{
    public class LoginUseCase : IRequestHandler<AuthenticationRequest, bool>
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

        public async Task<bool> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            var authenticationResponse = await _authenticator.Authenticate();
            var result = false;
            AuthenticationResponse useCaseResponse;

            if (authenticationResponse.Succeeded)
            {
                await _secureDataRepository.SetPersonalAccessToken(authenticationResponse.Payload).ConfigureAwait(false);

                // Fetch and store user's profile info             
                await _profileRepository.InsertOrReplace(await _gitHubGraphQLApi.GetGitHubViewer().ConfigureAwait(false)).ConfigureAwait(false);
                await _openStandupApi.SaveProfile();
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
