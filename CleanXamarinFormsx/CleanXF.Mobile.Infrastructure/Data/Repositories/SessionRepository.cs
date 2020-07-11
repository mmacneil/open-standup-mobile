using CleanXF.Core.Interfaces.Data.Repositories;
using System.Threading.Tasks;

namespace CleanXF.Mobile.Infrastructure.Data.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly AppDb _appDb;

        public SessionRepository(AppDb appDb)
        {
            _appDb = appDb;
        }

        public async Task<bool> Initialize(string accessToken)
        {
            try
            {
                int row = 0;
                await _appDb.AsyncDb.RunInTransactionAsync(tran =>
                {
                    tran.Execute("delete from session");
                    row = tran.Execute("insert into session values (?)", accessToken);
                });

                return row == 1;
            }
            catch { return false; }
        }

        public async Task Delete()
        {
            await _appDb.AsyncDb.ExecuteAsync("delete from session");
        }

        public async Task<bool> HasAccessToken()
        {
            return await _appDb.AsyncDb.ExecuteScalarAsync<int>("select 1 from session where AccessToken != '' OR AccessToken is not null") == 1;
        }
    }
}
