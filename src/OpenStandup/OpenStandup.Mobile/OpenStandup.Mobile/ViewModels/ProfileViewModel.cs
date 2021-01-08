using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using OpenStandup.Core.Domain.Entities;
using OpenStandup.Core.Domain.Features.Profile.Models;
using OpenStandup.Mobile.Models;
using OpenStandup.SharedKernel.Extensions;


namespace OpenStandup.Mobile.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public IList<StatModel> StatModels { get; private set; }

        private bool _initialized;
        public bool Initialized
        {
            get => _initialized;
            set => SetAndRaisePropertyChanged(ref _initialized, value);
        }

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

        private bool _showActions;
        public bool ShowActions
        {
            get => _showActions;
            set => SetAndRaisePropertyChanged(ref _showActions, value);
        }

        private bool _canFollow;
        public bool CanFollow
        {
            get => _canFollow;
            set => SetAndRaisePropertyChanged(ref _canFollow, value);
        }

        private bool _isFollowing;
        public bool IsFollowing
        {
            get => _isFollowing;
            set => SetAndRaisePropertyChanged(ref _isFollowing, value);
        }

        public string SelectedLogin, SelectedGitHubId;
        private string _gitHubId;
        
        private readonly IMediator _mediator;

        public ProfileViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Initialize()
        {
            _gitHubId = SelectedGitHubId;
            ShowActions = false;
            Initialized = false;
            IsBusy = true;
            StatusText = "Loading...";
            await _mediator.Send(new GetProfileRequest(SelectedGitHubId, SelectedLogin)).ConfigureAwait(false);
        }

        public void SetData(GitHubUser gitHubUser)
        {
            AvatarUrl = gitHubUser.AvatarUrl;
            Login = gitHubUser.Login.Truncate(13, true);
            Location = gitHubUser.Location;
            Joined = $"Joined {gitHubUser.CreatedAt:MMM dd, yyyy}";
            Email = gitHubUser.Email;
            WebsiteUrl = gitHubUser.WebsiteUrl;

            StatModels = new List<StatModel>
            {
                new StatModel ("followers", gitHubUser.FollowerCount),
                new StatModel ("following", gitHubUser.FollowingCount),
                new StatModel ("repos", gitHubUser.RepositoryCount),
                new StatModel ("gists", gitHubUser.GistCount)
            };

            Initialized = true;
        }

        public async Task UpdateFollower(bool follow=false)
        {
            await _mediator.Send(new UpdateFollowerRequest(_gitHubId, Login, follow));
        }
    }
}
