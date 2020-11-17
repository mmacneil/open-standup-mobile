using System.Collections.Generic;
using System.Linq;
using OpenStandup.Core.Interfaces.Data.GraphQL;
using OpenStandup.Core.Interfaces.Data.Repositories;
using OpenStandup.Mobile.Infrastructure.Data.GraphQL.Responses;
using GraphQL;
using GraphQL.Client.Http;
using System.Threading.Tasks;
using AutoMapper;
using OpenStandup.Mobile.Infrastructure.Data.GraphQL.Types;
using Vessel;
using GitHubUser = OpenStandup.Core.Domain.Entities.GitHubUser;


namespace OpenStandup.Mobile.Infrastructure.Apis
{
    public class GitHubGraphQLApi : BaseApi, IGitHubGraphQLApi
    {
        private const int PageSize = 100;
        private readonly GraphQLHttpClient _graphQLHttpClient;
        private readonly IMapper _mapper;

        public GitHubGraphQLApi(GraphQLHttpClient graphQLHttpClient, IMapper mapper, ISecureDataRepository secureDataRepository) : base(secureDataRepository)
        {
            _graphQLHttpClient = graphQLHttpClient;
            _mapper = mapper;
        }

        public async Task<Dto<GitHubUser>> GetViewer()
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
                                        },
                                        gists {
                                            totalCount
                                        }                                       
                                    }
                                }"
            };

            await AddAuthorizationHeader(_graphQLHttpClient.HttpClient);

            var response = await Policies.AttemptAndRetryPolicy(() => _graphQLHttpClient.SendQueryAsync<GitHubViewerGraphQLResponse>(graphQLRequest))
                .ConfigureAwait(false);

            if (!response.IsSuccess())
            {
                return response.GenerateFailedResponse<GitHubViewerGraphQLResponse, GitHubUser>();
            }

            // Use a separate sub-query method to extract repos
            var repos = await GetViewerRepositories();
            if (repos.Succeeded)
            {
                response.Data.Viewer.Repositories = new RepositoriesConnection(repos.Payload, null, repos.Payload.Count);
            }

            return Dto<GitHubUser>.Success(_mapper.Map<GitHubUser>(response.Data.Viewer));
        }

        private async Task<Dto<ICollection<Repository>>> GetViewerRepositories()
        {
            var graphQLRequest = new GraphQLRequest
            {
                Query = @"query ViewerRepositories($first: Int!, $after: String) {
                          viewer {
                            repositories(first: $first, after: $after) {
                              nodes {
                                databaseId
                                name
                                url
                                isPrivate
                              }
                              pageInfo {
                                endCursor
                                hasNextPage
                              }
                            }
                          }
                        }",
                OperationName = "ViewerRepositories",
                Variables = new
                {
                    first = PageSize
                }
            };

            await AddAuthorizationHeader(_graphQLHttpClient.HttpClient);

            var hasNextPage = true;
            var results = new List<Repository>();

            while (hasNextPage)
            {
                var response = await Policies.AttemptAndRetryPolicy(() => _graphQLHttpClient.SendQueryAsync<GitHubViewerGraphQLResponse>(graphQLRequest));

                if (response.IsSuccess())
                {
                    results.AddRange(response.Data.Viewer.Repositories.Nodes);
                    if (response.Data.Viewer.Repositories.PageInfo.HasNextPage)
                    {
                        graphQLRequest.Variables = new { first = PageSize, after = response.Data.Viewer.Repositories.PageInfo.EndCursor };
                    }
                    else
                    {
                        hasNextPage = false;
                    }
                }
                else
                {
                    break;
                }
            }

            return results.Any()
                ? Dto<ICollection<Repository>>.Success(results)
                : Dto<ICollection<Repository>>.Failed();
        }
    }
}


