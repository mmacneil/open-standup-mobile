using System.Threading.Tasks;
using OpenStandup.Core.Domain.Entities;
using OpenStandup.Core.Interfaces;
using OpenStandup.Core.Interfaces.Data.Repositories;

namespace OpenStandup.Mobile.Infrastructure
{
    public class AppContext : IAppContext
    {
        public GitHubUser User { get; private set; }

        private readonly IProfileRepository _profileRepository;

        public AppContext(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task Refresh()
        {
            User = await _profileRepository.Get().ConfigureAwait(false);
        }
    }
}
