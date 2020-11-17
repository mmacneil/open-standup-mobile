using System.Threading.Tasks;
using OpenStandup.Core.Domain.Entities;
using OpenStandup.Core.Interfaces;
using OpenStandup.Core.Interfaces.Data.Repositories;

namespace OpenStandup.Mobile.Infrastructure
{
    public class AppContext : IAppContext
    {
        public GitHubUser User { get; private set; }

        private readonly IUserRepository _userRepository;

        public AppContext(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Refresh()
        {
            User = await _userRepository.Get().ConfigureAwait(false);
        }
    }
}
