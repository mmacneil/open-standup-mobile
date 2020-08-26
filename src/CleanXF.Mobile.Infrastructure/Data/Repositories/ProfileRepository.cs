using CleanXF.Core.Domain.Entities;
using CleanXF.Core.Domain.Values;
using CleanXF.Core.Interfaces.Data.Repositories;
using CleanXF.Mobile.Infrastructure.Data.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CleanXF.Mobile.Infrastructure.Data.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AppDb _appDb;

        public ProfileRepository(AppDb appDb)
        {
            _appDb = appDb;
        }

        public async Task<bool> Insert(GitHubUser user)
        {
            return await _appDb.AsyncDb.InsertAsync(new Profile
            {
                Name = user.Name,
                Login = user.Login,
                AvatarUrl = user.AvatarUrl,
                BioHTML = user.BioHTML,
                Company = user.Company,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                Location = user.Location,
                FollowerCount = user.Followers.TotalCount,
                FollowingCount = user.Following.TotalCount
            }) == 1;
        }

        public async Task<GitHubUser> Get()
        {
            Profile model = (await _appDb.AsyncDb.QueryAsync<Profile>("select * from profile")).FirstOrDefault();

            if (model != null)
            {
                return new GitHubUser(model.Login, model.Name, model.AvatarUrl, model.BioHTML, model.Company, model.Location, model.Email, model.CreatedAt, new Followers(model.FollowerCount), new Following(model.FollowingCount));
            }

            throw new Exception("No profile exists.");
        }
    }
}
