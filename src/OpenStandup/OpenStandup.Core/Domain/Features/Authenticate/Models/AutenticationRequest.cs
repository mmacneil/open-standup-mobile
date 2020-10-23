using OpenStandup.Core.Interfaces;
using MediatR;


namespace OpenStandup.Core.Domain.Features.Authenticate.Models
{
    public class AuthenticationRequest : BaseUseCaseRequest<AuthenticationResponse>, IRequest<AuthenticationResponse>
    {
        public AuthenticationRequest(IOutputPort<AuthenticationResponse> outputPort) : base(outputPort)
        {
        }
    }
}
