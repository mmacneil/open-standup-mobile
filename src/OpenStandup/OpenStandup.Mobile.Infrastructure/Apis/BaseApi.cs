using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using OpenStandup.Core.Interfaces.Data.Repositories;

namespace OpenStandup.Mobile.Infrastructure.Apis
{
    public abstract class BaseApi
    {
        private readonly ISecureDataRepository _secureDataRepository;

        protected BaseApi(ISecureDataRepository secureDataRepository)
        {
            _secureDataRepository = secureDataRepository ?? throw new ArgumentNullException(nameof(secureDataRepository));
        }

        public async Task AddAuthorizationHeader(HttpRequestMessage request)
        {
            request.Headers.Add("Authorization", await _secureDataRepository.GetPersonalAccessToken().ConfigureAwait(false));
        }

        public async Task AddAuthorizationHeader(HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await _secureDataRepository.GetPersonalAccessToken().ConfigureAwait(false));
        }
    }
}


