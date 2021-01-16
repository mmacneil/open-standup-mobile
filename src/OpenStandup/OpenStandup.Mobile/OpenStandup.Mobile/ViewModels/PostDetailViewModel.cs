using System.Collections.Generic;
using System.Threading.Tasks;
using OpenStandup.Common.Dto;
using OpenStandup.Core.Interfaces.Apis;
using OpenStandup.Mobile.Interfaces;


namespace OpenStandup.Mobile.ViewModels
{
    public class PostDetailViewModel : BaseViewModel
    {
        public IList<CommentDto> Comments { get; private set; }

        private bool _canPost;
        public bool CanPost
        {
            get => _canPost;
            set => SetAndRaisePropertyChanged(ref _canPost, value);
        }

        private string _commentText;
        public string CommentText
        {
            get => _commentText;
            set => SetAndRaisePropertyChanged(ref _commentText, value);
        }

        public int Id;

        private readonly IIndicatorPageService _indicatorPageService;
        private readonly IOpenStandupApi _openStandupApi;

        public PostDetailDto Post;

        public PostDetailViewModel(IIndicatorPageService indicatorPageService, IOpenStandupApi openStandupApi)
        {
            _indicatorPageService = indicatorPageService;
            _openStandupApi = openStandupApi;
        }

        public async Task Initialize()
        {
            var post = await _openStandupApi.GetPost(Id).ConfigureAwait(false);

            if (post.Succeeded)
            {
                Post = post.Payload;
                Comments = new List<CommentDto>(post.Payload.Comments);
                OnPropertyChanged(nameof(Comments));
            }
        }

        public async Task PublishComment()
        {
            //_indicatorPageService.ShowIndicatorPage();
            await _openStandupApi.PublishPostComment(Id, _commentText);
            // await _mediator.Send(new PublishPostRequest(Text, PhotoPath));
            // _indicatorPageService.HideIndicatorPage();
        }
    }
}
