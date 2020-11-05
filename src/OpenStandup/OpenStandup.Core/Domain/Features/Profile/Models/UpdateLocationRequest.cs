using MediatR;
using Vessel;

namespace OpenStandup.Core.Domain.Features.Profile.Models
{
    public class UpdateLocationRequest : IRequest<Dto<bool>>
    {
    }
}