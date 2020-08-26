using CleanXF.Core.Domain.Entities;
using CleanXF.Core.Interfaces.Data.GraphQL;
using CleanXF.Core.Interfaces.Data.Repositories;
using CleanXF.Mobile.Infrastructure.Data.GraphQL.Responses;
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

        public async Task<GitHubUser> GetGitHubViewer()
        {
            var graphQLRequest = new GraphQLRequest
            {
                Query = @"query { viewer 
                                    { 
                                        login, 
                                        name, 
                                        avatarUrl, 
                                        bioHTML,
                                        company,
                                        email, 
                                        createdAt,
                                        location,
                                        followers {
                                            totalCount
                                        },
                                        following {
                                            totalCount
                                        }
                                    }
                                }"
            };

            _graphQLHttpClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await _sessionRepository.GetAccessToken().ConfigureAwait(false));
            return (await _graphQLHttpClient.SendQueryAsync<GitHubViewerGraphQLResponse>(graphQLRequest).ConfigureAwait(false)).Data.Viewer;
        }
    }
}
