using MediatR;
using OpenStandup.SharedKernel;


namespace OpenStandup.Core.Domain.Features.Authenticate.Models
{
    public class AuthenticationRequest : IRequest<Result<string>>
    {
    }
}
