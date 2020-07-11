using MediatR;


namespace CleanXF.Core.Domain.Features.Signout.Models
{
    public class SignoutRequest : BaseUseCaseRequest<bool>, IRequest<bool>
    {
    }
}
