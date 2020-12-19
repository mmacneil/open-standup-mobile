using OpenStandup.Core.Domain.Entities;
using OpenStandup.Core.Interfaces.Data.Repositories;
using OpenStandup.Mobile.Infrastructure.Data.Model;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace OpenStandup.Mobile.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDb _appDb;

        public UserRepository(AppDb appDb)
        {
            _appDb = appDb;
        }

        public async Task<bool> InsertOrReplace(GitHubUser user)
        {
            await _appDb.AsyncDb.RunInTransactionAsync(tran =>
            {
                tran.InsertOrReplace(new ProfileData
                {
                    Id = user.Id,
                    Name = user.Name,
                    Login = user.Login,
                    AvatarUrl = user.AvatarUrl,
                    BioHtml = user.BioHtml,
                    WebsiteUrl = user.WebsiteUrl,
                    Company = user.Company,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt,
                    Location = user.Location,
                    FollowerCount = user.FollowerCount,
                    FollowingCount = user.FollowingCount,
                    RepositoryCount = user.RepositoryCount,
                    GistCount = user.GistCount
                });

                if (!user.Repositories.Any()) return;
                tran.Execute("DELETE from repositories");
                tran.InsertAll(user.Repositories.Select(r => new RepositoryData
                { Id = r.Id, Name = r.Name, Url = r.Url, IsPrivate = r.IsPrivate }));

            }).ConfigureAwait(false);

            return true;
        }

        public async Task<GitHubUser> Get()
        {
            var model = (await _appDb.AsyncDb.QueryAsync<ProfileData>("select * from profile").ConfigureAwait(false)).FirstOrDefault();

            if (model != null)
            {
                return new GitHubUser(model.Id, model.Login, model.Name, model.AvatarUrl, model.BioHtml, model.WebsiteUrl, model.Company, model.Location, model.DatabaseId, model.Email, model.CreatedAt, model.FollowerCount, model.RepositoryCount, model.FollowingCount, model.GistCount, model.Latitude ?? 0, model.Longitude ?? 0, null, default, default);
            }

            throw new Exception("No profile exists.");
        }

        public async Task<bool> UpdateLocation(string id, double latitude, double longitude)
        {
            var user = await _appDb.AsyncDb.GetAsync<ProfileData>(id);
            user.Latitude = latitude;
            user.Longitude = longitude;
            return await _appDb.AsyncDb.UpdateAsync(user).ConfigureAwait(false) == 1;
        }
    }
}
