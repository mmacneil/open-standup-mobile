using AutoMapper;
using OpenStandup.Core.Domain.Entities;
using OpenStandup.Core.Dto;

namespace OpenStandup.Mobile.Infrastructure.Mapping
{
	public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<GitHubUser, UserDto>()
                .ConstructUsing(user => new UserDto(user.Name, user.Login, user.AvatarUrl, user.BioHTML,
                    user.WebsiteUrl, user.Company, user.Email, user.CreatedAt, user.Location, user.Followers.TotalCount,
                    user.Following.TotalCount, user.Repositories.TotalCount, user.Gists.TotalCount, user.DatabaseId,
                    user.Id));
            // .ReverseMap()
            // .ConstructUsing(userDto => new User(userDto.Id, userDto.Name));
        }
    }
}

 