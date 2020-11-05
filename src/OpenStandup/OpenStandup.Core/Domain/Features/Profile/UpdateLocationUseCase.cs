using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStandup.Core.Domain.Features.Profile.Models;
using OpenStandup.Core.Interfaces;
using OpenStandup.Core.Interfaces.Apis;
using OpenStandup.Core.Interfaces.Data.GraphQL;
using OpenStandup.Core.Interfaces.Data.Repositories;
using OpenStandup.SharedKernel;
using Vessel;

namespace OpenStandup.Core.Domain.Features.Profile
{
    public class UpdateLocationUseCase : IRequestHandler<UpdateLocationRequest, Dto<bool>>
    {
        private readonly ILocationService _locationService;
        private readonly IOpenStandupApi _openStandupApi;
      
        

        public UpdateLocationUseCase(ILocationService locationService, IOpenStandupApi openStandupApi)
        {
            _locationService = locationService;
            _openStandupApi = openStandupApi;

        }

        public async Task<Dto<bool>> Handle(UpdateLocationRequest request, CancellationToken cancellationToken)
        {
            Dto<bool> useCaseResponse;
            var (latitude, longitude) = await _locationService.GetCurrentLocation(new CancellationTokenSource());
            await _openStandupApi.UpdateLocation(latitude, longitude);
            return Dto<bool>.Failed();
        }
    }
}


