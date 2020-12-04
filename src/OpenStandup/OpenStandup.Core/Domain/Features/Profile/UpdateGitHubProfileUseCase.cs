using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStandup.Core.Domain.Events;
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
        private readonly IUserRepository _userRepository;
        private readonly IOpenStandupApi _openStandupApi;
        private readonly IOutputPort<Dto<bool>> _outputPort;
        private readonly IMediator _mediator;

        public UpdateGitHubProfileUseCase(IGitHubGraphQLApi gitHubGraphQLApi, IUserRepository userRepository, IOpenStandupApi openStandupApi, IOutputPort<Dto<bool>> outputPort, IMediator mediator)
        {
            _gitHubGraphQLApi = gitHubGraphQLApi;
            _userRepository = userRepository;
            _openStandupApi = openStandupApi;
            _outputPort = outputPort;
            _mediator = mediator;
        }

        public async Task<Dto<bool>> Handle(UpdateGitHubProfileRequest request, CancellationToken cancellationToken)
        {
            Dto<bool> useCaseResponse;

            // Fetch and store user's profile info   
            var gitHubUserResponse = await _gitHubGraphQLApi.GetViewer().ConfigureAwait(false);

            if (gitHubUserResponse.Succeeded)
            {
                await _userRepository.InsertOrReplace(gitHubUserResponse.Payload).ConfigureAwait(false);
                var apiResponse = await _openStandupApi.UpdateProfile(gitHubUserResponse.Payload).ConfigureAwait(false);

                if (apiResponse.Succeeded)
                {
                    await _mediator.Publish(new ProfileUpdated(), cancellationToken).ConfigureAwait(false);
                    await _outputPort.Handle(apiResponse);
                    return apiResponse;
                }

                useCaseResponse = apiResponse;
            }
            else
            {
                useCaseResponse = Dto<bool>.Failed(gitHubUserResponse.Status,
                    gitHubUserResponse.Exception != null
                        ? gitHubUserResponse.Exception.Message
                        : gitHubUserResponse.Errors.FirstOrDefault());
            }

            await _outputPort.Handle(useCaseResponse);
            return useCaseResponse;
        }
    }
}


