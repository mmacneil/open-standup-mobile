using OpenStandup.Core.Domain.Entities;
using OpenStandup.Core.Interfaces.Data.GraphQL;
using OpenStandup.Core.Interfaces.Data.Repositories;
using OpenStandup.Mobile.Infrastructure.Data.GraphQL.Responses;
using GraphQL;
using GraphQL.Client.Http;
using System.Threading.Tasks;
using OpenStandup.SharedKernel;
using OpenStandup.SharedKernel.Extensions;


namespace OpenStandup.Mobile.Infrastructure.Apis
{
    public class GitHubGraphQLApi : BaseApi, IGitHubGraphQLApi
    {
        private readonly GraphQLHttpClient _graphQLHttpClient;

        public GitHubGraphQLApi(GraphQLHttpClient graphQLHttpClient, ISecureDataRepository secureDataRepository) : base(secureDataRepository)
        {
            _graphQLHttpClient = graphQLHttpClient;
        }

        public async Task<Result<GitHubUser>> GetGitHubViewer()
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

            await AddAuthorizationHeader(_graphQLHttpClient.HttpClient);

            var response = await Policies.AttemptAndRetryPolicy(() => _graphQLHttpClient.SendQueryAsync<GitHubViewerGraphQLResponse>(graphQLRequest))
                .ConfigureAwait(false);

            return response.Data.Exception == null
                ? Result<GitHubUser>.Success(response.Data.Viewer)
                : Result<GitHubUser>.Failed(response.Data.Exception.StatusCode.ToResultStatus(), response.Data.Exception);
        }
    }
}


