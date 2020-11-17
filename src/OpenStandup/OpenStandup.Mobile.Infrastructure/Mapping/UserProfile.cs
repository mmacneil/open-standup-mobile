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
                       user.Followers.TotalCount,
                       user.Repositories.TotalCount,
                       user.Following.TotalCount,
                       user.Gists.TotalCount,
                       user.Latitude,
                       user.Longitude, user.Repositories.Nodes.Select(r => new Repository(r.DatabaseId, r.Name, r.Url, r.IsPrivate))));

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
                    user.Id));
        }
    }
}

