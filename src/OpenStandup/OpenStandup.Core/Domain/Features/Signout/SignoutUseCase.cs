using System.Threading;
using System.Threading.Tasks;
using OpenStandup.Core.Domain.Features.Signout.Models;
using OpenStandup.Core.Interfaces.Data.Repositories;
using MediatR;

namespace OpenStandup.Core.Domain.Features.Signout
{
    public class SignoutUseCase : IRequestHandler<SignoutRequest, bool>
    {
        private readonly ISecureDataRepository _secureDataRepository;

        public SignoutUseCase(ISecureDataRepository secureDataRepository)
        {
            _secureDataRepository = secureDataRepository;
        }

        public Task<bool> Handle(SignoutRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_secureDataRepository.RemovePersonalAccessToken());
        }
    }
}
