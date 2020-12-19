using System;
using System.Collections.Generic;
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

        public async Task<Dto<bool>> Follow(string userId)
        {
            var graphQLRequest = new GraphQLRequest
            {
                Query = @"mutation FollowUser($input: FollowUserInput!) {
                            followUser(input: $input) {
                                clientMutationId
                            }
                        }",
                OperationName = "FollowUser",
                Variables =
                    new
                    {
                        input = new
                        {
                            clientMutationId = Guid.NewGuid().ToString(),
                            userId
                        }
                    }
            };

            await AddAuthorizationHeader(_graphQLHttpClient.HttpClient);

            var response = await Policies.AttemptAndRetryPolicy(() => _graphQLHttpClient.SendQueryAsync<FollowUserGraphQLResponse>(graphQLRequest))
                .ConfigureAwait(false);

            return !response.IsSuccess() ? response.GenerateFailedResponse<FollowUserGraphQLResponse, bool>() : Dto<bool>.Success(true);
        }

        public async Task<Dto<bool>> Unfollow(string userId)
        {
            var graphQLRequest = new GraphQLRequest
            {
                Query = @"mutation UnfollowUser($input: UnfollowUserInput!) {
                            unfollowUser(input: $input) {
                                clientMutationId
                            }
                        }",
                OperationName = "UnfollowUser",
                Variables =
                    new
                    {
                        input = new
                        {
                            clientMutationId = Guid.NewGuid().ToString(),
                            userId
                        }
                    }
            };

            await AddAuthorizationHeader(_graphQLHttpClient.HttpClient);

            var response = await Policies.AttemptAndRetryPolicy(() => _graphQLHttpClient.SendQueryAsync<FollowUserGraphQLResponse>(graphQLRequest))
                .ConfigureAwait(false);

            return !response.IsSuccess() ? response.GenerateFailedResponse<FollowUserGraphQLResponse, bool>() : Dto<bool>.Success(true);
        }

        public async Task<Dto<GitHubUser>> GetFollowerStatus(string login)
        {
            var graphQLRequest = new GraphQLRequest
            {
                Query = @"query FollowerStatus($login: String!) {
                          user(login:$login) {
                                viewerCanFollow,
                                viewerIsFollowing
                              }
                        }",
                OperationName = "FollowerStatus",
                Variables = new
                {
                    login
                }
            };

            await AddAuthorizationHeader(_graphQLHttpClient.HttpClient);

            var response = await Policies.AttemptAndRetryPolicy(() => _graphQLHttpClient.SendQueryAsync<GitHubUserGraphQLResponse>(graphQLRequest))
                .ConfigureAwait(false);

            return !response.IsSuccess() ? response.GenerateFailedResponse<GitHubUserGraphQLResponse, GitHubUser>() : Dto<GitHubUser>.Success(_mapper.Map<GitHubUser>(response.Data.User));
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
            response.Data.Viewer.Repositories = new RepositoriesConnection(repos.Payload, null, repos.Payload.Count);
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

            return Dto<ICollection<Repository>>.Success(results);
        }
    }
}


