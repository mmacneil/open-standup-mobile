using CleanXF.Core.Interfaces.Data.GraphQL;
using System.Threading.Tasks;

namespace CleanXF.Mobile.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly IGitHubGraphQLApi _gitHubGraphQLApi;

        public ProfileViewModel(IGitHubGraphQLApi gitHubGraphQLApi)
        {
            _gitHubGraphQLApi = gitHubGraphQLApi;
        }

        public async Task Initialize()
        {
           var user = await _gitHubGraphQLApi.GetGitHubUser("mmacneil").ConfigureAwait(false);
            bool here = true;
        }
    }
}
