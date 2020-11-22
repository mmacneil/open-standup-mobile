using System.Threading;
using System.Threading.Tasks;
using OpenStandup.Core.Domain.Features.Logout.Models;
using OpenStandup.Core.Interfaces.Data.Repositories;
using MediatR;

namespace OpenStandup.Core.Domain.Features.Logout
{
    public class LogoutUseCase : IRequestHandler<LogoutRequest, bool>
    {
        private readonly ISecureDataRepository _secureDataRepository;

        public LogoutUseCase(ISecureDataRepository secureDataRepository)
        {
            _secureDataRepository = secureDataRepository;
        }

        public Task<bool> Handle(LogoutRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_secureDataRepository.RemovePersonalAccessToken());
        }
    }
}
