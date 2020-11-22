using System.Threading.Tasks;
using OpenStandup.Core.Interfaces.Services;
using OpenStandup.Mobile.Interfaces;

namespace OpenStandup.Mobile.ViewModels
{
    public class EditPostViewModel : BaseViewModel
    {
        private readonly ICameraService _cameraService;
        private readonly IDialogProvider _dialogProvider;

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
            set
            {
                CanPost = value.Length >= 5;
                SetAndRaisePropertyChanged(ref _text, value);
            }
        }

        private bool _canPost;
        public bool CanPost
        {
            get => _canPost;
            set => SetAndRaisePropertyChanged(ref _canPost, value);
        }

        public EditPostViewModel(ICameraService cameraService, IDialogProvider dialogProvider)
        {
            _cameraService = cameraService;
            _dialogProvider = dialogProvider;
        }

        public async Task Initialize()
        {
        }

        public async Task TakePhoto()
        {
            PhotoPath = await _cameraService.TakePhotoAsync();
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
