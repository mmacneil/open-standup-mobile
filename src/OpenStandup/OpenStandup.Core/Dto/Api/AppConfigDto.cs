using System;

namespace OpenStandup.Core.Dto.Api
{
    public class AppConfigDto
    {
        public string GitHubClientId { get; }
        public string GitHubClientSecret { get; }
        public string Version { get; }
        public DateTimeOffset Created { get; }


        public AppConfigDto(string gitHubClientId, string gitHubClientSecret, string version, DateTimeOffset created)
        {
            GitHubClientId = gitHubClientId;
            GitHubClientSecret = gitHubClientSecret;
            Version = version;
            Created = created;
        }
    }
}


 