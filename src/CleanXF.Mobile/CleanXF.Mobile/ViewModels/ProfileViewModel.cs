using CleanXF.Core.Interfaces.Data.GraphQL;
using CleanXF.Core.Interfaces.Data.Repositories;
using System.Threading.Tasks;

namespace CleanXF.Mobile.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
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
            var me = await _profileRepository.Get();
            bool here = true;
        }
    }
}
