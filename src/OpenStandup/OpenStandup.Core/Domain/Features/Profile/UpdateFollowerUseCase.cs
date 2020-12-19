using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStandup.Core.Domain.Features.Profile.Models;
using OpenStandup.Core.Interfaces;
using OpenStandup.Core.Interfaces.Data.GraphQL;


namespace OpenStandup.Core.Domain.Features.Profile
{
    public class UpdateFollowerUseCase : IRequestHandler<UpdateFollowerRequest, Unit>
    {
        private readonly IGitHubGraphQLApi _gitHubGraphQLApi;
        private readonly IOutputPort<UpdateFollowerResponse> _outputPort;

        public UpdateFollowerUseCase(IGitHubGraphQLApi gitHubGraphQLApi, IOutputPort<UpdateFollowerResponse> outputPort)
        {
            _gitHubGraphQLApi = gitHubGraphQLApi;
            _outputPort = outputPort;
        }

        public async Task<Unit> Handle(UpdateFollowerRequest request, CancellationToken cancellationToken)
        {
            await _outputPort.Handle(new UpdateFollowerResponse(request, request.Follow
                ? await _gitHubGraphQLApi.Follow(request.GitHubId).ConfigureAwait(false)
                : await _gitHubGraphQLApi.Unfollow(request.GitHubId).ConfigureAwait(false)));

            return new Unit();
        }
    }
}


 