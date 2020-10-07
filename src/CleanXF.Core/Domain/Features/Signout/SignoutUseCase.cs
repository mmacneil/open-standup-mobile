using System.Threading;
using System.Threading.Tasks;
using CleanXF.Core.Domain.Features.Signout.Models;
using CleanXF.Core.Interfaces.Data.Repositories;
using MediatR;

namespace CleanXF.Core.Domain.Features.Signout
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
