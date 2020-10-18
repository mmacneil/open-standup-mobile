using OpenStandup.Core.Domain.Entities;
using OpenStandup.Core.Interfaces.Data.GraphQL;
using OpenStandup.Core.Interfaces.Data.Repositories;
using OpenStandup.Mobile.Infrastructure.Data.GraphQL.Responses;
using GraphQL;
using GraphQL.Client.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace OpenStandup.Mobile.Infrastructure.Data.GraphQL
{
    public class GitHubGraphQLApi : IGitHubGraphQLApi
    {
        private readonly GraphQLHttpClient _graphQLHttpClient;
        private readonly ISecureDataRepository _secureDataRepository;

        public GitHubGraphQLApi(GraphQLHttpClient graphQLHttpClient, ISecureDataRepository secureDataRepository)
        {
            _graphQLHttpClient = graphQLHttpClient;
            _secureDataRepository = secureDataRepository;
        }

        public async Task<GitHubUser> GetGitHubViewer()
        {
            var graphQLRequest = new GraphQLRequest
            {
                Query = @"query { viewer 
                                    { 
                                        id,
                                        login, 
                                        name, 
                                        avatarUrl, 
                                        bioHTML,
                                        websiteUrl,
                                        company,
                                        email, 
                                        createdAt,
                                        location,
                                        databaseId,
                                        followers {
                                            totalCount
                                        },
                                        following {
                                            totalCount
                                        },
                                        repositories {
                                            totalCount
                                        }
                                        gists {
                                            totalCount
                                        }                                       
                                    }
                                }"
            };

            _graphQLHttpClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await _secureDataRepository.GetPersonalAccessToken().ConfigureAwait(false));
            return (await _graphQLHttpClient.SendQueryAsync<GitHubViewerGraphQLResponse>(graphQLRequest).ConfigureAwait(false)).Data.Viewer;
        }
    }
}
