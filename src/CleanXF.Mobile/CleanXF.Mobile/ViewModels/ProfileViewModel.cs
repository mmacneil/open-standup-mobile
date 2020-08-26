using CleanXF.Core.Interfaces.Data.GraphQL;
using System.Threading.Tasks;

namespace CleanXF.Mobile.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly IGitHubGraphQLApi _gitHubGraphQLApi;

        private string _avatarUrl;

        public string AvatarUrl
        {
            get => _avatarUrl;
            set => SetAndRaisePropertyChanged(ref _avatarUrl, value);
        }

        public ProfileViewModel(IGitHubGraphQLApi gitHubGraphQLApi)
        {
            _gitHubGraphQLApi = gitHubGraphQLApi;
        }

        public async Task Initialize()
        {
            //var user = await _gitHubGraphQLApi.GetGitHubUser("mmacneil").ConfigureAwait(false);
            //AvatarUrl = user.AvatarUrl;            
        }
    }
}
