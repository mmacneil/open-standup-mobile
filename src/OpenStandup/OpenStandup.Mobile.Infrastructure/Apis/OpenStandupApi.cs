using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OpenStandup.Core.Domain.Entities;
using OpenStandup.Core.Interfaces;
using OpenStandup.Core.Interfaces.Apis;
using Newtonsoft.Json;
using OpenStandup.Common;
using OpenStandup.Common.Dto;
using OpenStandup.Core.Interfaces.Data.Repositories;
using Vessel;


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

        public async Task<Dto<bool>> UpdateProfile(GitHubUser gitHubUser)
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
                ? Dto<bool>.Success(true)
                : Dto<bool>.Failed(response.StatusCode, response.ReasonPhrase);
        }

        public async Task<Dto<bool>> UpdateLocation(double latitude, double longitude)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, $"{_appSettings.ApiEndpoint}/users/Location")
            {
                Content = new StringContent(JsonConvert.SerializeObject(new { latitude, longitude }), Encoding.UTF8, "application/json")
            };

            await AddAuthorizationHeader(request);

            var response = await Policies.AttemptAndRetryPolicy(() => _httpClient.SendAsync(request))
                .ConfigureAwait(false);

            return response.IsSuccessStatusCode
                ? Dto<bool>.Success(true)
                : Dto<bool>.Failed(response.StatusCode, response.ReasonPhrase);
        }

        public async Task<Dto<AppConfigDto>> GetConfiguration()
        {
            var response = await Policies.AttemptAndRetryPolicy(() => _httpClient.GetAsync($"{_appSettings.ApiEndpoint}/configuration")).ConfigureAwait(false);

            return response.IsSuccessStatusCode
                ? Dto<AppConfigDto>.Success(JsonConvert.DeserializeObject<AppConfigDto>(await response.Content.ReadAsStringAsync().ConfigureAwait(false)))
                : Dto<AppConfigDto>.Failed(response.StatusCode, response.ReasonPhrase);
        }

        public async Task<Dto<string>> ValidateGitHubAccessToken(string token)
        {
            var response = await Policies.AttemptAndRetryPolicy(() => _httpClient.PostAsync($"{_appSettings.ApiEndpoint}/users/ValidateGitHubAccessToken", new StringContent($"\"{token}\"", Encoding.UTF8, "application/json"))).ConfigureAwait(false);

            return response.IsSuccessStatusCode
                ? Dto<string>.Success(response.StatusCode.ToString())
                : Dto<string>.Failed(response.StatusCode);
        }

        public async Task<Dto<bool>> PublishPost(string text, byte[] image)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_appSettings.ApiEndpoint}/posts")
            {
                Content = new StringContent(JsonConvert.SerializeObject(new PostDto(text, image)), Encoding.UTF8, "application/json")
            };

            await AddAuthorizationHeader(request);

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            return response.IsSuccessStatusCode
                ? Dto<bool>.Success(true)
                : Dto<bool>.Failed(response.StatusCode, response.ReasonPhrase);
        }

        public async Task<Dto<PagedResult<PostSummaryDto>>> GetPostSummaries(int offset)
        {
            var response = await Policies.AttemptAndRetryPolicy(() => _httpClient.GetAsync($"{_appSettings.ApiEndpoint}/posts/summaries?offset={offset}")).ConfigureAwait(false);

            return response.IsSuccessStatusCode ?
                Dto<PagedResult<PostSummaryDto>>.Success(
                    JsonConvert.DeserializeObject<PagedResult<PostSummaryDto>>(
                        await response.Content.ReadAsStringAsync().ConfigureAwait(false)))
            : Dto<PagedResult<PostSummaryDto>>.Failed();
        }

        public async Task<Dto<GitHubUser>> GetUser(string gitHubId)
        {
            var response = await Policies.AttemptAndRetryPolicy(() => _httpClient.GetAsync($"{_appSettings.ApiEndpoint}/users?gitHubId={gitHubId}")).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return Dto<GitHubUser>.Success(_mapper.Map<GitHubUser>(JsonConvert.DeserializeObject<UserDto>(
                    await response.Content.ReadAsStringAsync())));
            }

            return Dto<GitHubUser>.Failed(response.StatusCode, response.ReasonPhrase);
        }
    }
}
