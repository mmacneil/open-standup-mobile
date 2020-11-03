using MediatR;
using OpenStandup.SharedKernel;


namespace OpenStandup.Core.Domain.Features.SaveProfile.Models
{
    public class GetGitHubProfileRequest : IRequest<Result<bool>>
    {
    }
}
