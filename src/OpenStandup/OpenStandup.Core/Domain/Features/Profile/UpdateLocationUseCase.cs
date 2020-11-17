using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStandup.Core.Domain.Events;
using OpenStandup.Core.Domain.Features.Profile.Models;
using OpenStandup.Core.Interfaces;
using OpenStandup.Core.Interfaces.Apis;
using OpenStandup.Core.Interfaces.Data.Repositories;
using OpenStandup.Core.Interfaces.Services;
using Vessel;

namespace OpenStandup.Core.Domain.Features.Profile
{
    public class UpdateLocationUseCase : IRequestHandler<UpdateLocationRequest, Dto<bool>>
    {
        private readonly ILocationService _locationService;
        private readonly IOpenStandupApi _openStandupApi;
        private readonly IAppContext _appContext;
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;

        public UpdateLocationUseCase(ILocationService locationService, IOpenStandupApi openStandupApi, IAppContext appContext, IUserRepository userRepository, IMediator mediator)
        {
            _locationService = locationService;
            _openStandupApi = openStandupApi;
            _appContext = appContext;
            _userRepository = userRepository;
            _mediator = mediator;
        }

        public async Task<Dto<bool>> Handle(UpdateLocationRequest request, CancellationToken cancellationToken)
        {
            var (latitude, longitude) = await _locationService.GetCurrentLocation(new CancellationTokenSource()).ConfigureAwait(false);
            await _openStandupApi.UpdateLocation(latitude, longitude).ConfigureAwait(false);
            await _userRepository.UpdateLocation(_appContext.User.Id, latitude, longitude).ConfigureAwait(false);
            await _mediator.Publish(new ProfileUpdated(), cancellationToken).ConfigureAwait(false);
            return Dto<bool>.Success(true);
        }
    }
}


