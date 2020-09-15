using CleanXF.Core.Domain.Entities;
using CleanXF.Core.Interfaces.Data.Repositories;
using System.Threading.Tasks;

namespace CleanXF.Mobile.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private GitHubUser _me;
        public GitHubUser Me
        {
            get => _me;
            set => SetAndRaisePropertyChanged(ref _me, value);
        }       

        private readonly IProfileRepository _profileRepository;

        private string _avatarUrl;

        public string AvatarUrl
        {
            get => _avatarUrl;
            set => SetAndRaisePropertyChanged(ref _avatarUrl, value);
        }

        public ProfileViewModel(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task Initialize()
        {
            Me = await _profileRepository.Get();
            AvatarUrl = Me.AvatarUrl;
        }
    }
}
