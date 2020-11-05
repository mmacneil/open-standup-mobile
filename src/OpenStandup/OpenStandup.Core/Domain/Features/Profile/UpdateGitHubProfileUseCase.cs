using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStandup.Core.Domain.Features.Profile.Models;
using OpenStandup.Core.Interfaces;
using OpenStandup.Core.Interfaces.Apis;
using OpenStandup.Core.Interfaces.Data.GraphQL;
using OpenStandup.Core.Interfaces.Data.Repositories;
using Vessel;

namespace OpenStandup.Core.Domain.Features.Profile
{
    public class UpdateGitHubProfileUseCase : IRequestHandler<UpdateGitHubProfileRequest, Dto<bool>>
    {
        private readonly IGitHubGraphQLApi _gitHubGraphQLApi;
        private readonly IProfileRepository _profileRepository;
        private readonly IOpenStandupApi _openStandupApi;
        private readonly IOutputPort<Dto<bool>> _outputPort;

        public UpdateGitHubProfileUseCase(IGitHubGraphQLApi gitHubGraphQLApi, IProfileRepository profileRepository, IOpenStandupApi openStandupApi, IOutputPort<Dto<bool>> outputPort)
        {
            _gitHubGraphQLApi = gitHubGraphQLApi;
            _profileRepository = profileRepository;
            _openStandupApi = openStandupApi;
            _outputPort = outputPort;
        }

        public async Task<Dto<bool>> Handle(UpdateGitHubProfileRequest request, CancellationToken cancellationToken)
        {
            Dto<bool> useCaseResponse;

            // Fetch and store user's profile info   
            var gitHubUserResponse = await _gitHubGraphQLApi.GetGitHubViewer().ConfigureAwait(false);

            if (gitHubUserResponse.Succeeded)
            {
                await _profileRepository.InsertOrReplace(gitHubUserResponse.Payload).ConfigureAwait(false);
                useCaseResponse = await _openStandupApi.UpdateProfile(gitHubUserResponse.Payload).ConfigureAwait(false);
            }
            else
            {
                useCaseResponse = Dto<bool>.Failed(gitHubUserResponse.Status, gitHubUserResponse.Exception.Message);
            }

            _outputPort.Handle(useCaseResponse);

            return useCaseResponse;
        }
    }
}


