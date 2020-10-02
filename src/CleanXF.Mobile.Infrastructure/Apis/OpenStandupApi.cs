using System.Net.Http;
using System.Threading.Tasks;
using CleanXF.Core.Interfaces.Apis;


namespace CleanXF.Mobile.Infrastructure.Apis
{
    public class OpenStandupApi : IOpenStandupApi
    {
        private readonly HttpClient _httpClient;

        public OpenStandupApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SaveProfile()
        {
            //Now let's call our retry policy each time we want to query the API
            var response = await Policies.AttemptAndRetryPolicy(() => _httpClient.GetAsync($"{Configuration.ServiceEndpoint}/api/users"));

            return true;
        }

        public async Task<bool> GetConfiguration()
        {
            //Now let's call our retry policy each time we want to query the API
            var response = await Policies.AttemptAndRetryPolicy(() => _httpClient.GetAsync($"{Configuration.ServiceEndpoint}/api/configuration"));

            return true;
        }
    }
}
