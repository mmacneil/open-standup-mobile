using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OpenStandup.Common.Dto;
using GitHubUser = OpenStandup.Mobile.Infrastructure.Data.GraphQL.Types.GitHubUser;
using Repository = OpenStandup.Core.Domain.Entities.Repository;


namespace OpenStandup.Mobile.Infrastructure.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<GitHubUser, Core.Domain.Entities.GitHubUser>()
                   .ConvertUsing(user => new Core.Domain.Entities.GitHubUser(
                       user.Id,
                       user.Login,
                       user.Name,
                       user.AvatarUrl,
                       user.BioHtml,
                       user.WebsiteUrl,
                       user.Company,
                       user.Location,
                       user.DatabaseId,
                       user.Email,
                       user.CreatedAt,
                       user.Followers == null ? 0 : user.Followers.TotalCount,
                       user.Repositories == null ? 0 : user.Repositories.TotalCount,
                       user.Following == null ? 0 : user.Following.TotalCount,
                       user.Gists == null ? 0 : user.Gists.TotalCount,
                       user.Latitude,
                       user.Longitude, user.Repositories == null ? new List<Repository>() : user.Repositories.Nodes.Select(r => new Repository(r.DatabaseId, r.Name, r.Url, r.IsPrivate)), user.ViewerCanFollow, user.ViewerIsFollowing));

            CreateMap<Core.Domain.Entities.GitHubUser, UserDto>()
                .ConvertUsing(user => new UserDto(
                    user.Name,
                    user.Login,
                    user.AvatarUrl,
                    user.BioHtml,
                    user.WebsiteUrl,
                    user.Company,
                    user.Email,
                    user.CreatedAt,
                    user.Location,
                    user.FollowerCount,
                    user.FollowingCount,
                    user.RepositoryCount,
                    user.GistCount,
                    user.DatabaseId,
                    user.Id, user.Repositories.Select(r => new RepositoryDto(r.Id, r.Name, r.Url, r.IsPrivate)).ToList()));

            CreateMap<UserDto, Core.Domain.Entities.GitHubUser>()
                .ConvertUsing(user => new Core.Domain.Entities.GitHubUser(
                    user.GitHubId,
                    user.Login,
                    user.Name,
                    user.AvatarUrl,
                    user.BioHtml,
                    user.WebsiteUrl,
                    user.Company,
                    user.Location,
                    user.DatabaseId,
                    user.Email,
                    user.CreatedAt,
                    user.FollowerCount,
                    user.RepositoryCount,
                    user.FollowingCount,
                    user.GistCount,
                    default, default, default, default, default));
        }
    }
}

