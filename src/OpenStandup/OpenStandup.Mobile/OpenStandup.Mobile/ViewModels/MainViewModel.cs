using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MediatR;
using OpenStandup.Common.Dto;
using OpenStandup.Common.Extensions;
using OpenStandup.Core.Interfaces;
using OpenStandup.Core.Interfaces.Apis;

namespace OpenStandup.Mobile.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<PostSummaryDto> PostSummaries { get; private set; } = new ObservableCollection<PostSummaryDto>();

        private readonly IAppContext _appContext;
        private readonly IMediator _mediator;
        private readonly IOpenStandupApi _openStandupApi;

        public MainViewModel(IAppContext appContext, IMediator mediator, IOpenStandupApi openStandupApi)
        {
            _appContext = appContext;
            _mediator = mediator;
            _openStandupApi = openStandupApi;
        }

        public async Task Initialize()
        {
            var summaries = await _openStandupApi.GetPostSummaries();
            PostSummaries = new ObservableCollection<PostSummaryDto>(summaries.Payload);
            OnPropertyChanged(nameof(PostSummaries));
            /*foreach (var s in summaries.Payload)
            {
                PostSummaries.Add(s);
            }*/
        }
    }
}
