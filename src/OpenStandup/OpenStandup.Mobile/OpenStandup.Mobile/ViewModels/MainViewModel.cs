using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using OpenStandup.Common.Dto;
using OpenStandup.Core.Interfaces.Apis;

namespace OpenStandup.Mobile.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<PostDto> PostSummaries { get; } = new ObservableCollection<PostDto>();

        private readonly IOpenStandupApi _openStandupApi;
        private int _offset;

        private int _itemThreshold;

        public int ItemThreshold
        {
            get => _itemThreshold;
            set => SetProperty(ref _itemThreshold, value);
        }

        public MainViewModel(IOpenStandupApi openStandupApi)
        {
            _openStandupApi = openStandupApi;
        }

        public async Task Initialize()
        {
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
                        foreach (var summary in summaries.Payload.Items)
                        {
                            PostSummaries.Add(summary);
                        }
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
