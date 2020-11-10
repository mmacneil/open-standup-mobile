using OpenStandup.Core.Domain.Entities;
using OpenStandup.Core.Interfaces.Data.GraphQL;
using OpenStandup.Core.Interfaces.Data.Repositories;
using OpenStandup.Mobile.Infrastructure.Data.GraphQL.Responses;
using GraphQL;
using GraphQL.Client.Http;
using System.Threading.Tasks;
using Vessel;


namespace OpenStandup.Mobile.Infrastructure.Apis
{
    public class GitHubGraphQLApi : BaseApi, IGitHubGraphQLApi
    {
        private readonly GraphQLHttpClient _graphQLHttpClient;

        public GitHubGraphQLApi(GraphQLHttpClient graphQLHttpClient, ISecureDataRepository secureDataRepository) : base(secureDataRepository)
        {
            _graphQLHttpClient = graphQLHttpClient;
        }

        public async Task<Dto<GitHubUser>> GetGitHubViewer()
        {
            var graphQLRequest = new GraphQLRequest
            {
                Query = @"query GitHubUser($first: Int!) { viewer 
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
                                        repositories(first: $first) {
                                            totalCount,
                                            nodes {
                                                databaseId,
                                                name,
                                                url,
                                                isPrivate
                                            }  
                                        }
                                        gists {
                                            totalCount
                                        }                                       
                                    }
                                }",
                OperationName = "GitHubUser",
                Variables = new
                {
                    first = 100
                }
            };

            await AddAuthorizationHeader(_graphQLHttpClient.HttpClient);

            var response = await Policies.AttemptAndRetryPolicy(() => _graphQLHttpClient.SendQueryAsync<GitHubViewerGraphQLResponse>(graphQLRequest))
                .ConfigureAwait(false);

            return response.IsSuccess()
                ? Dto<GitHubUser>.Success(response.Data.Viewer)
                : response.GenerateFailedResponse<GitHubViewerGraphQLResponse, GitHubUser>();
        }
    }
}


