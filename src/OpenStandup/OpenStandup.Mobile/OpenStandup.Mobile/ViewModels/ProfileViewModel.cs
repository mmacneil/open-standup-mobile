using System.Collections.Generic;
using System.Threading.Tasks;
using OpenStandup.Core.Interfaces;
using OpenStandup.Core.Interfaces.Apis;
using OpenStandup.Mobile.Models;
using OpenStandup.SharedKernel.Extensions;

namespace OpenStandup.Mobile.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
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

        public string AuthorLogin;

        private readonly IAppContext _appContext;
        private readonly IOpenStandupApi _openStandupApi;

        public ProfileViewModel(IAppContext appContext, IOpenStandupApi openStandupApi)
        {
            _appContext = appContext;
            _openStandupApi = openStandupApi;
        }

        public async Task Initialize()
        {
            if (AuthorLogin == null || AuthorLogin == _appContext.User.Login)
            {
                AvatarUrl = _appContext.User.AvatarUrl;
                Login = _appContext.User.Login.Truncate(13, true);
                Location = _appContext.User.Location;
                Joined = $"Joined {_appContext.User.CreatedAt:MMM dd, yyyy}";
                Email = _appContext.User.Email;
                WebsiteUrl = _appContext.User.WebsiteUrl;

                StatModels = new List<StatModel>
                {
                    new StatModel ("followers", _appContext.User.FollowerCount),
                    new StatModel ("following", _appContext.User.FollowingCount),
                    new StatModel ("repositories", _appContext.User.RepositoryCount),
                    new StatModel ("gists", _appContext.User.GistCount)
                };
            }
            else
            {
                await _openStandupApi.GetUser(AuthorLogin);
            }

            AuthorLogin = null; // fix me so "local" view doesn't need this reset
        }
    }
}
