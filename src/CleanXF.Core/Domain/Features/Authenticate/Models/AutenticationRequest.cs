using MediatR;


namespace CleanXF.Core.Domain.Features.Authenticate.Models
{
    public class AuthenticationRequest : BaseUseCaseRequest<AuthenticationResponse>, IRequest<bool>
    {
    }
}
