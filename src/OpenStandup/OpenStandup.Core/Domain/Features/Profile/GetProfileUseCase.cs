using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStandup.Core.Domain.Entities;
using OpenStandup.Core.Domain.Features.Profile.Models;
using OpenStandup.Core.Interfaces;
using OpenStandup.Core.Interfaces.Apis;
using OpenStandup.Core.Interfaces.Data.GraphQL;
using Vessel;


namespace OpenStandup.Core.Domain.Features.Profile
{
    public class GetProfileUseCase : IRequestHandler<GetProfileRequest, Unit>
    {
        private readonly IAppContext _appContext;
        private readonly IGitHubGraphQLApi _gitHubGraphQLApi;
        private readonly IOpenStandupApi _openStandupApi;
        private readonly IOutputPort<Dto<GetProfileResponse>> _outputPort;

        public GetProfileUseCase(IAppContext appContext, IGitHubGraphQLApi gitHubGraphQLApi, IOpenStandupApi openStandupApi, IOutputPort<Dto<GetProfileResponse>> outputPort)
        {
            _appContext = appContext;
            _gitHubGraphQLApi = gitHubGraphQLApi;
            _openStandupApi = openStandupApi;
            _outputPort = outputPort;
        }

        public async Task<Unit> Handle(GetProfileRequest request, CancellationToken cancellationToken)
        {
            if (request.GitHubId == null || request.GitHubId == _appContext.User.Id)
            {
                await _outputPort.Handle(Dto<GetProfileResponse>.Success(new GetProfileResponse(Dto<GitHubUser>.Success(_appContext.User))));
            }
            else
            {
                var apiResponse = await _openStandupApi.GetUser(request.GitHubId).ConfigureAwait(false);

                if (apiResponse.Succeeded)
                {
                    var followerStatusResponse = await _gitHubGraphQLApi.GetFollowerStatus(request.Login).ConfigureAwait(false);
                    await _outputPort.Handle(Dto<GetProfileResponse>.Success(new GetProfileResponse(apiResponse, followerStatusResponse.Payload.ViewerCanFollow, followerStatusResponse.Payload.ViewerIsFollowing)))
                        .ConfigureAwait(false);
                }
                else
                {
                    await _outputPort.Handle(Dto<GetProfileResponse>.Failed(apiResponse.Status, $"Failed to fetch {request.Login}'s profile. ({apiResponse.Errors.FirstOrDefault()})"));
                }
            }

            return new Unit();
        }
    }
}

