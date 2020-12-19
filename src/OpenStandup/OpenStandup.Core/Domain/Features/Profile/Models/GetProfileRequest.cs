using MediatR;


namespace OpenStandup.Core.Domain.Features.Profile.Models
{
    public class GetProfileRequest : IRequest<Unit>
    {
        public string GitHubId { get; }
        public string Login { get; }

        public GetProfileRequest(string gitHubId, string login)
        {
            GitHubId = gitHubId;
            Login = login;
        }
    }
}
