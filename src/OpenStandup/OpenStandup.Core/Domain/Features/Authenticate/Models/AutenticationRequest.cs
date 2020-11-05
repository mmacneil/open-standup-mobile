using MediatR;
using Vessel;

namespace OpenStandup.Core.Domain.Features.Authenticate.Models
{
    public class AuthenticationRequest : IRequest<Dto<string>>
    {
    }
}
