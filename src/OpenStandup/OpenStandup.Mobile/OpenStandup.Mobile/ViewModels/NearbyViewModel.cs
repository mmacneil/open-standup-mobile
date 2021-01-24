using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using OpenStandup.Common.Dto;
using OpenStandup.Core.Domain.Features.Profile.Models;
using OpenStandup.Core.Interfaces;
using OpenStandup.Core.Interfaces.Apis;

namespace OpenStandup.Mobile.ViewModels
{
    public class NearbyViewModel : BaseViewModel
    {
        public IEnumerable<UserNearbyDto> UsersNearby { get; private set; }

        private readonly IAppContext _appContext;
        private readonly IMediator _mediator;
        private readonly IOpenStandupApi _openStandupApi;

        public NearbyViewModel(IAppContext appContext, IMediator mediator, IOpenStandupApi openStandupApi)
        {
            _appContext = appContext;
            _mediator = mediator;
            _openStandupApi = openStandupApi;
        }

        public async Task Initialize()
        {
            IsBusy = true;

            // Fetch & save location
            if (_appContext.RequiresRefresh || _appContext.RequiresLocation)
            {
                await _mediator.Send(new UpdateLocationRequest());
            }

            var users = await _openStandupApi.GetNearbyUsers().ConfigureAwait(false);

            if (users.Succeeded)
            {
                UsersNearby = users.Payload;
                OnPropertyChanged(nameof(UsersNearby));
            }

            IsBusy = false;
        }
    }
}
