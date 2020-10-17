using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanXF.Core.Domain.Entities;
using CleanXF.Core.Dto;
using CleanXF.Core.Dto.Api;
using CleanXF.Core.Interfaces;
using CleanXF.Core.Interfaces.Apis;
using CleanXF.SharedKernel;
using Newtonsoft.Json;


namespace CleanXF.Mobile.Infrastructure.Apis
{
    public class OpenStandupApi : IOpenStandupApi
    {
        private readonly HttpClient _httpClient;
        private readonly IAppSettings _appSettings;
        private readonly IMapper _mapper;

        public OpenStandupApi(HttpClient httpClient, IAppSettings appSettings, IMapper mapper)
        {
            _httpClient = httpClient;
            _appSettings = appSettings;
            _mapper = mapper;
        }

        public async Task<HttpOperationResponse<string>> SaveProfile(GitHubUser gitHubUser)
        {
            var userDto = _mapper.Map<UserDto>(gitHubUser);

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_appSettings.ApiEndpoint}/users")
            {
                Content = new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8, "application/json")
            };

            var response = await Policies.AttemptAndRetryPolicy(() => _httpClient.SendAsync(request)).ConfigureAwait(false);

            return new HttpOperationResponse<string>(response.StatusCode, response.IsSuccessStatusCode ? OperationResult.Succeeded : OperationResult.Failed, null);
        }

        public async Task<OperationResponse<AppConfigDto>> GetConfiguration()
        {
            var response = await Policies.AttemptAndRetryPolicy(() => _httpClient.GetAsync($"{_appSettings.ApiEndpoint}/configuration")).ConfigureAwait(false);

            return !response.IsSuccessStatusCode ?
                new OperationResponse<AppConfigDto>(OperationResult.Failed, null) :
                new OperationResponse<AppConfigDto>(OperationResult.Succeeded, JsonConvert.DeserializeObject<AppConfigDto>(await response.Content.ReadAsStringAsync()));
        }

        public async Task<HttpOperationResponse<string>> ValidateGitHubAccessToken(string token)
        {
            var response = await Policies.AttemptAndRetryPolicy(() => _httpClient.GetAsync($"{_appSettings.ApiEndpoint}/users/ValidateGitHubAccessToken?token={token}"));

            return response.IsSuccessStatusCode
                ? new HttpOperationResponse<string>(response.StatusCode, OperationResult.Succeeded, null)
                : new HttpOperationResponse<string>(response.StatusCode, OperationResult.Failed, null);
        }
    }
}
