using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OpenStandup.Core.Domain.Entities;
using OpenStandup.Core.Dto;
using OpenStandup.Core.Dto.Api;
using OpenStandup.Core.Interfaces;
using OpenStandup.Core.Interfaces.Apis;
using OpenStandup.SharedKernel;
using Newtonsoft.Json;
using OpenStandup.Core.Interfaces.Data.Repositories;
using OpenStandup.SharedKernel.Extensions;


namespace OpenStandup.Mobile.Infrastructure.Apis
{
    public class OpenStandupApi : BaseApi, IOpenStandupApi
    {
        private readonly HttpClient _httpClient;
        private readonly IAppSettings _appSettings;
        private readonly IMapper _mapper;

        public OpenStandupApi(HttpClient httpClient, IAppSettings appSettings, IMapper mapper, ISecureDataRepository secureDataRepository) : base(secureDataRepository)
        {
            _httpClient = httpClient;
            _appSettings = appSettings;
            _mapper = mapper;
        }

        public async Task<Result<bool>> SaveProfile(GitHubUser gitHubUser)
        {
            var userDto = _mapper.Map<UserDto>(gitHubUser);

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_appSettings.ApiEndpoint}/users")
            {
                Content = new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8, "application/json")
            };

            await AddAuthorizationHeader(request);

            var response = await Policies.AttemptAndRetryPolicy(() => _httpClient.SendAsync(request))
                .ConfigureAwait(false);

            return response.IsSuccessStatusCode
                ? Result<bool>.Success(true)
                : Result<bool>.Failed(response.StatusCode.ToResultStatus(), response.ReasonPhrase);
        }

        public async Task<Result<AppConfigDto>> GetConfiguration()
        {
            var response = await Policies.AttemptAndRetryPolicy(() => _httpClient.GetAsync($"{_appSettings.ApiEndpoint}/configuration")).ConfigureAwait(false);

            return response.IsSuccessStatusCode
                ? Result<AppConfigDto>.Success(JsonConvert.DeserializeObject<AppConfigDto>(await response.Content.ReadAsStringAsync().ConfigureAwait(false)))
                : Result<AppConfigDto>.Failed(response.StatusCode.ToResultStatus(), response.ReasonPhrase);
        }

        public async Task<Result<string>> ValidateGitHubAccessToken(string token)
        {
            var response = await Policies.AttemptAndRetryPolicy(() => _httpClient.GetAsync($"{_appSettings.ApiEndpoint}/users/ValidateGitHubAccessToken?token={token}")).ConfigureAwait(false);

            return response.IsSuccessStatusCode
                ? Result<string>.Success(response.StatusCode.ToString())
                : Result<string>.Failed(response.StatusCode.ToResultStatus());
        }
    }
}
