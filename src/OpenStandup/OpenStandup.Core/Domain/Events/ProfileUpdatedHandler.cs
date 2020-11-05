using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStandup.Core.Interfaces;

namespace OpenStandup.Core.Domain.Events
{
    public class ProfileUpdatedHandler : INotificationHandler<ProfileUpdated>
    {
        private readonly IAppContext _appContext;

        public ProfileUpdatedHandler(IAppContext appContext)
        {
            _appContext = appContext;
        }

        public Task Handle(ProfileUpdated notification, CancellationToken cancellationToken)
        {
            return _appContext.Refresh();
        }
    }
}
