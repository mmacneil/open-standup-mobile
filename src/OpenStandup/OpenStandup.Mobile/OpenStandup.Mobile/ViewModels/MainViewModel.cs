using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OpenStandup.Common.Dto;
using OpenStandup.Core.Domain.Features.Profile.Models;
using OpenStandup.Core.Interfaces;
using OpenStandup.Core.Interfaces.Apis;

namespace OpenStandup.Mobile.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public IList<PostDto> PostSummaries { get; private set; } = new List<PostDto>();

        private readonly IAppContext _appContext;
        private readonly IMediator _mediator;
        private readonly IOpenStandupApi _openStandupApi;
        private int _offset;

        private int _itemThreshold;

        public int ItemThreshold
        {
            get => _itemThreshold;
            set => SetProperty(ref _itemThreshold, value);
        }

        public MainViewModel(IAppContext appContext, IMediator mediator, IOpenStandupApi openStandupApi)
        {
            _appContext = appContext;
            _mediator = mediator;
            _openStandupApi = openStandupApi;
        }

        public async Task Initialize()
        {
            // Fetch & save location
            if (_appContext.RequiresRefresh || _appContext.RequiresLocation)
            {
                await _mediator.Send(new UpdateLocationRequest());
            }

            await _mediator.Send(new UpdateLocationRequest());
            Reset();
            await LoadPostSummaries();
        }

        public void Reset()
        {
            _offset = 0;
            PostSummaries.Clear();
            ItemThreshold = 1;
        }

        public async Task LoadPostSummaries()
        {
            try
            {
                IsBusy = true;
                var summaries = await _openStandupApi.GetPostSummaries(_offset).ConfigureAwait(false);

                if (summaries.Succeeded)
                {
                    if (!summaries.Payload.Items.Any())
                    {
                        ItemThreshold = -1;
                    }
                    else
                    {
                        PostSummaries = new List<PostDto>(summaries.Payload.Items);
                        OnPropertyChanged(nameof(PostSummaries));
                    }

                    _offset = summaries.Payload.Offset;
                }
            }
            catch
            {

            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
