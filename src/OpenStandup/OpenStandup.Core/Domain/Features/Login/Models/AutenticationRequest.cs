using MediatR;
using Vessel;

namespace OpenStandup.Core.Domain.Features.Login.Models
{
    public class AuthenticationRequest : IRequest<Dto<string>>
    {
    }
}
