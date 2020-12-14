using System;
using System.Threading.Tasks;
using OpenStandup.Core.Domain.Entities;
using OpenStandup.Core.Interfaces;
using OpenStandup.Core.Interfaces.Data.Repositories;
using OpenStandup.Mobile.Infrastructure.Services;

namespace OpenStandup.Mobile.Infrastructure
{
    public class AppContext : IAppContext
    {
        public GitHubUser User { get; private set; }
        public bool RequiresRefresh => _lastRefresh == null || _lastRefresh.Value <= DateTimeOffset.UtcNow.AddMilliseconds(-JobService.RefreshInterval);
        public bool RequiresLocation => User != null && User.Longitude == 0;

        private DateTimeOffset? _lastRefresh;

        private readonly IUserRepository _userRepository;

        public AppContext(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Refresh()
        {
            User = await _userRepository.Get().ConfigureAwait(false);
            _lastRefresh = DateTimeOffset.UtcNow;
        }
    }
}
