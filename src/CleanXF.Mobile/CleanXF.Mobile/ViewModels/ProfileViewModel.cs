using System.Collections.Generic;
using CleanXF.Core.Interfaces.Data.Repositories;
using System.Threading.Tasks;
using CleanXF.Mobile.Models;

namespace CleanXF.Mobile.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly IProfileRepository _profileRepository;

        public IList<StatModel> StatModels { get; private set; }

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

        private string _email;
        public string Email
        {
            get => _email;
            set => SetAndRaisePropertyChanged(ref _email, value);
        }

        private string _websiteUrl;
        public string WebsiteUrl
        {
            get => _websiteUrl;
            set => SetAndRaisePropertyChanged(ref _websiteUrl, value);
        }

        public ProfileViewModel(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task Initialize()
        {
            var me = await _profileRepository.Get().ConfigureAwait(false);
            AvatarUrl = me.AvatarUrl;
            Login = me.Login;
            Location = me.Location;
            Joined = $"Joined {me.CreatedAt:MMM dd, yyyy}";
            Email = me.Email;
            WebsiteUrl = me.WebsiteUrl;

           StatModels = new List<StatModel>
            {
                new StatModel ("followers", me.Followers.TotalCount),
                new StatModel ("following", me.Following.TotalCount),
                new StatModel ("repositories", me.Repositories.TotalCount),
                new StatModel ("gists", me.Gists.TotalCount)
            };
        }
    }
}
