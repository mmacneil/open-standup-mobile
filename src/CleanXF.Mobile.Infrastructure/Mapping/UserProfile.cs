using AutoMapper;
using CleanXF.Core.Domain.Entities;
using CleanXF.Core.Dto;

namespace CleanXF.Mobile.Infrastructure.Mapping
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

 