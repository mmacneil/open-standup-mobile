using MediatR;


namespace OpenStandup.Core.Domain.Features.Signout.Models
{
    public class SignoutRequest : BaseUseCaseRequest<bool>, IRequest<bool>
    {
    }
}
