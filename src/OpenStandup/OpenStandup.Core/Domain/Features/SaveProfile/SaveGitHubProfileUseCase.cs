using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStandup.Core.Domain.Features.SaveProfile.Models;
using OpenStandup.Core.Interfaces;
using OpenStandup.Core.Interfaces.Apis;
using OpenStandup.Core.Interfaces.Data.GraphQL;
using OpenStandup.Core.Interfaces.Data.Repositories;
using OpenStandup.SharedKernel;


namespace OpenStandup.Core.Domain.Features.SaveProfile
{
    public class SaveGitHubProfileUseCase : IRequestHandler<SaveGitHubProfileRequest, Result<bool>>
    {
        private readonly IGitHubGraphQLApi _gitHubGraphQLApi;
        private readonly IProfileRepository _profileRepository;
        private readonly IOpenStandupApi _openStandupApi;
        private readonly IOutputPort<Result<bool>> _outputPort;

        public SaveGitHubProfileUseCase(IGitHubGraphQLApi gitHubGraphQLApi, IProfileRepository profileRepository, IOpenStandupApi openStandupApi, IOutputPort<Result<bool>> outputPort)
        {
            _gitHubGraphQLApi = gitHubGraphQLApi;
            _profileRepository = profileRepository;
            _openStandupApi = openStandupApi;
            _outputPort = outputPort;
        }

        public async Task<Result<bool>> Handle(SaveGitHubProfileRequest request, CancellationToken cancellationToken)
        {
            Result<bool> useCaseResponse;

            // Fetch and store user's profile info   
            var gitHubUserResponse = await _gitHubGraphQLApi.GetGitHubViewer().ConfigureAwait(false);

            if (gitHubUserResponse.Succeeded)
            {
                await _profileRepository.InsertOrReplace(gitHubUserResponse.Value).ConfigureAwait(false);
                useCaseResponse = await _openStandupApi.SaveProfile(gitHubUserResponse.Value).ConfigureAwait(false);
            }
            else
            {
                useCaseResponse = Result<bool>.Failed(gitHubUserResponse.Status, gitHubUserResponse.Exception.Message);
            }

            _outputPort.Handle(useCaseResponse);

            return useCaseResponse;
        }
    }
}


