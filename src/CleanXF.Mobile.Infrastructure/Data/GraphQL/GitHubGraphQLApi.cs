using CleanXF.Core.Domain.Entities;
using CleanXF.Core.Interfaces.Data.GraphQL;
using CleanXF.Core.Interfaces.Data.Repositories;
using GraphQL;
using GraphQL.Client.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace CleanXF.Mobile.Infrastructure.Data.GraphQL
{
    public class GitHubGraphQLApi : IGitHubGraphQLApi
    {
        private readonly GraphQLHttpClient _graphQLHttpClient;
        private readonly ISessionRepository _sessionRepository;

        public GitHubGraphQLApi(GraphQLHttpClient graphQLHttpClient, ISessionRepository sessionRepository)
        {
            _graphQLHttpClient = graphQLHttpClient;
            _sessionRepository = sessionRepository;
        }

        public async Task<GitHubUser> GetGitHubUser(string login)
        {
            var graphQLRequest = new GraphQLRequest
            {
                Query = "query { user(login: \"" + login + "\"){ name, avatarUrl, company, createdAt, followers{ totalCount }}}"
            };

            _graphQLHttpClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await _sessionRepository.GetAccessToken().ConfigureAwait(false));
            var gitHubUserResponse = await _graphQLHttpClient.SendQueryAsync<GitHubUserGraphQLResponse>(graphQLRequest).ConfigureAwait(false);
            return gitHubUserResponse.Data.User;
        }
    }
}
