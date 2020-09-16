using CleanXF.Core.Domain.Entities;
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

        private string _login;
        public string Login
        {
            get => _login;
            set => SetAndRaisePropertyChanged(ref _login, value);
        }

        private string _location;
        public string Location
        {
            get => _location;
            set => SetAndRaisePropertyChanged(ref _location, value);
        }

        private string _joined;
        public string Joined
        {
            get => _joined;
            set => SetAndRaisePropertyChanged(ref _joined, value);
        }

        private long _followers;
        public long Followers
        {
            get => _followers;
            set => SetAndRaisePropertyChanged(ref _followers, value);
        }

        private long _following;
        public long Following
        {
            get => _following;
            set => SetAndRaisePropertyChanged(ref _following, value);
        }

        private long _repositories;
        public long Repositories
        {
            get => _repositories;
            set => SetAndRaisePropertyChanged(ref _repositories, value);
        }

        private long _gists;
        public long Gists
        {
            get => _gists;
            set => SetAndRaisePropertyChanged(ref _gists, value);
        }

        public ProfileViewModel(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task Initialize()
        {
            GitHubUser me = await _profileRepository.Get().ConfigureAwait(false);
            AvatarUrl = me.AvatarUrl;
            Login = me.Login;
            Location = me.Location;
            Joined = $"Joined {me.CreatedAt:MMM dd, yyyy}";
            Followers = me.Followers.TotalCount;
            Following = me.Following.TotalCount;
            Repositories = me.Repositories.TotalCount;
            Gists = me.Gists.TotalCount;
        }
    }
}
