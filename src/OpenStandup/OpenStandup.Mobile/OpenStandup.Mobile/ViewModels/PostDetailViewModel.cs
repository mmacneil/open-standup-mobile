using System.Threading.Tasks;
using OpenStandup.Common.Dto;
using OpenStandup.Core.Interfaces.Apis;


namespace OpenStandup.Mobile.ViewModels
{
    public class PostDetailViewModel : BaseViewModel
    {
        public int Id;

        private readonly IOpenStandupApi _openStandupApi;

        public PostSummaryDto PostSummary;

        public PostDetailViewModel(IOpenStandupApi openStandupApi)
        {
            _openStandupApi = openStandupApi;
        }

        public async Task Initialize()
        {
            var post = await _openStandupApi.GetPost(Id).ConfigureAwait(false);

            if (post.Succeeded)
            {
                PostSummary = post.Payload;
            }
        }
    }
}
