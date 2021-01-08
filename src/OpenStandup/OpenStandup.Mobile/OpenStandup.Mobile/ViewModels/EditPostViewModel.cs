using System.Threading.Tasks;
using MediatR;
using OpenStandup.Core.Domain.Features.Posts.Models;
using OpenStandup.Core.Interfaces.Services;
using OpenStandup.Mobile.Interfaces;


namespace OpenStandup.Mobile.ViewModels
{
    public class EditPostViewModel : BaseViewModel
    {
        private readonly ICameraService _cameraService;
        private readonly IDialogProvider _dialogProvider;
        private readonly IMediator _mediator;
        private readonly IIndicatorPageService _indicatorPageService;

        private string _photoPath;
        public string PhotoPath
        {
            get => _photoPath;
            set => SetAndRaisePropertyChanged(ref _photoPath, value);
        }

        private string _text;
        public string Text
        {
            get => _text;
            set => SetAndRaisePropertyChanged(ref _text, value);
        }

        private bool _canPost;
        public bool CanPost
        {
            get => _canPost;
            set => SetAndRaisePropertyChanged(ref _canPost, value);
        }

        public EditPostViewModel(ICameraService cameraService, IDialogProvider dialogProvider, IMediator mediator, IIndicatorPageService indicatorPageService)
        {
            _cameraService = cameraService;
            _dialogProvider = dialogProvider;
            _mediator = mediator;
            _indicatorPageService = indicatorPageService;
        }

        public async Task TakePhoto()
        {
            PhotoPath = await _cameraService.TakePhotoAsync();
        }

        public async Task PublishPost()
        {
            _indicatorPageService.ShowIndicatorPage();
            await _mediator.Send(new PublishPostRequest(Text, PhotoPath));
            _indicatorPageService.HideIndicatorPage();
        }

        public async Task DeletePhoto()
        {
            if (await _dialogProvider.DisplayAlert("Remove Photo", "Delete this photo?", "Yes", "No"))
            {
                PhotoPath = null;
            }
        }
    }
}
