using MediatR;
using Vessel;

namespace OpenStandup.Core.Domain.Features.Profile.Models
{
    public class UpdateGitHubProfileRequest : IRequest<Dto<bool>>
    {
    }
}
