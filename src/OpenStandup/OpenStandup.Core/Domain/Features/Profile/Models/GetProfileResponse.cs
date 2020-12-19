using OpenStandup.Core.Domain.Entities;
using Vessel;


namespace OpenStandup.Core.Domain.Features.Profile.Models
{
    public class GetProfileResponse
    {
        public Dto<GitHubUser> User { get; }
        public bool CanFollow { get; }
        public bool IsFollowing { get; }

        public GetProfileResponse(Dto<GitHubUser> user, bool canFollow = false, bool isFollowing = false)
        {
            User = user;
            CanFollow = canFollow;
            IsFollowing = isFollowing;
        }
    }
}
