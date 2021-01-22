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
        private readonly IToastService _toastService;

        public PostDetailDto Post;

        public PostDetailViewModel(IIndicatorPageService indicatorPageService, IOpenStandupApi openStandupApi, IToastService toastService)
        {
            _indicatorPageService = indicatorPageService;
            _openStandupApi = openStandupApi;
            _toastService = toastService;
        }

        public async Task Initialize()
        {
            CommentText = "";

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
            _indicatorPageService.ShowIndicatorPage();

            if ((await _openStandupApi.PublishPostComment(Id, CommentText)).Succeeded)
            {
                await Initialize();
            }

            _indicatorPageService.HideIndicatorPage();
        }

        public async Task DeleteComment(int id)
        {
            if ((await _openStandupApi.DeletePostComment(id)).Succeeded)
            {
                await Initialize();
                _toastService.Show("Comment deleted");
            }
            else
            {
                _toastService.Show("Failed to delete comment");
            }
        }
    }
}
