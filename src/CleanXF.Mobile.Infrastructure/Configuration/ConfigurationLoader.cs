using System.Threading.Tasks;
using CleanXF.Mobile.Infrastructure.Data;

namespace CleanXF.Mobile.Infrastructure.Configuration
{
    public class ConfigurationLoader
    {
        private readonly AppDb _appDb;

        public ConfigurationLoader(AppDb appDb)
        {
            _appDb = appDb;
        }

        public async Task<bool> TryLoad()
        {
            var config = await _appDb.AsyncDb.GetAsync<Data.Model.Configuration>(1);

            return true;
        }
    }
}
