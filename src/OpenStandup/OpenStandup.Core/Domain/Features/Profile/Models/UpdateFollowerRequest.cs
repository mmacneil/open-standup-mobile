using MediatR;


namespace OpenStandup.Core.Domain.Features.Profile.Models
{
    public class UpdateFollowerRequest : IRequest<Unit>
    {
        public string GitHubId { get; }
        public string Login { get; }
        public bool Follow { get; }

        public UpdateFollowerRequest(string gitHubId, string login, bool follow = false)
        {
            GitHubId = gitHubId;
            Login = login;
            Follow = follow;
        }
    }
}
