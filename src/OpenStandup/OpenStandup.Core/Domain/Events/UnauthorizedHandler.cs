using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStandup.Core.Interfaces;


namespace OpenStandup.Core.Domain.Events
{
    public class UnauthorizedHandler : INotificationHandler<Unauthorized>
    {
        private readonly IOutputPort<Unauthorized> _outputPort;

        public UnauthorizedHandler(IOutputPort<Unauthorized> outputPort)
        {
            _outputPort = outputPort;
        }

        public Task Handle(Unauthorized notification, CancellationToken cancellationToken)
        {
            return _outputPort.Handle(notification);
        }
    }
}
