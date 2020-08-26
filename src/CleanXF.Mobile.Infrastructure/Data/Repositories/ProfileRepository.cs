using CleanXF.Core.Domain.Entities;
using CleanXF.Core.Interfaces.Data.Repositories;
using Polly.Utilities;
using System;
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

        public async Task Delete()
        {
            await _appDb.AsyncDb.ExecuteAsync("delete from session");
        }

        public async Task<bool> Insert(GitHubUser user)
        {
            try
            {
                bool result = await _appDb.AsyncDb.ExecuteScalarAsync<int>(@"insert into profile (
                                                                    Name, 
                                                                    Login, 
                                                                    AvatarUrl, 
                                                                    BioHTML, 
                                                                    Company, 
                                                                    Email, 
                                                                    CreatedAt, 
                                                                    Location, 
                                                                    FollowerCount, 
                                                                    FollowingCount) VALUES (?,?,?,?,?,?,?,?,?,?)",
                                                                  user.Name,
                                                                  user.Login,
                                                                  user.AvatarUrl,
                                                                  user.BioHTML,
                                                                  user.Company,
                                                                  user.Email,
                                                                  user.CreatedAt,
                                                                  user.Location,
                                                                  user.Followers.TotalCount,
                                                                  user.Following.TotalCount) == 1;

            }
            catch(Exception e)
            {
                var msg = e.ToString();
            }
            return await _appDb.AsyncDb.ExecuteScalarAsync<int>(@"insert into profile (
                                                                    Name, 
                                                                    Login, 
                                                                    AvatarUrl, 
                                                                    BioHTML, 
                                                                    Company, 
                                                                    Email, 
                                                                    CreatedAt, 
                                                                    Location, 
                                                                    FollowerCount, 
                                                                    FollowingCount) VALUES (?,?,?,?,?,?,?,?,?,?)",
                                                                    user.Name,
                                                                    user.Login,
                                                                    user.AvatarUrl,
                                                                    user.BioHTML,
                                                                    user.Company,
                                                                    user.Email,
                                                                    user.CreatedAt,
                                                                    user.Location,
                                                                    user.Followers.TotalCount,
                                                                    user.Following.TotalCount) == 1;
        }

        public async Task<string> GetAccessToken()
        {
            return await _appDb.AsyncDb.ExecuteScalarAsync<string>("select AccessToken from session").ConfigureAwait(false);
        }
    }
}
