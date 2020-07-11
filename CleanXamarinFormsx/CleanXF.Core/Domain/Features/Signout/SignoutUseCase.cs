using CleanXF.Core.Domain.Features.Signout.Models;
using CleanXF.Core.Interfaces.Data.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanXF.Core.Domain.Features.Authenticate
{
    public class SignoutUseCase : IRequestHandler<SignoutRequest, bool>
    {
        private readonly ISessionRepository _sessionRepository;

        public SignoutUseCase(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<bool> Handle(SignoutRequest request, CancellationToken cancellationToken)
        {
            await _sessionRepository.Delete(); // Remove the local access token
            return true;
        }
    }
}
