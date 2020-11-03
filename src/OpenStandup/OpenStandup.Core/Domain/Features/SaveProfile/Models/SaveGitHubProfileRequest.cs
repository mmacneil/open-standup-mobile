using MediatR;
using OpenStandup.SharedKernel;


namespace OpenStandup.Core.Domain.Features.SaveProfile.Models
{
    public class SaveGitHubProfileRequest : IRequest<Result<bool>>
    {
    }
}
