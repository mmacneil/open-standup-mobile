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
             Text = "I was using 10445 for the longest time creating PCRs. I was trying to Override mandatory reasons but they did not appear [DB: Believed to be a configuration issue]. then I noticed my vehicle number was not populating on a new PCR [DB: http://jira.medusamedical.com:8080/browse/IFU-2625] so I went back to the patient list screen where I saw it pre populated and hit log off. that’s when the crash occurred";
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
