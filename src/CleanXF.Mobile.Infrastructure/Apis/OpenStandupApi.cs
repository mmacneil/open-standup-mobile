﻿using System.Net.Http;
using System.Threading.Tasks;
using CleanXF.Core.Dto.Api;
using CleanXF.Core.Interfaces;
using CleanXF.Core.Interfaces.Apis;
using CleanXF.SharedKernel;


namespace CleanXF.Mobile.Infrastructure.Apis
{
    public class OpenStandupApi : IOpenStandupApi
    {
        private readonly HttpClient _httpClient;
        private readonly IAppSettings _appSettings;

        public OpenStandupApi(HttpClient httpClient, IAppSettings appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings;
        }

        public async Task<bool> SaveProfile()
        {
            //Now let's call our retry policy each time we want to query the API
            var response = await Policies.AttemptAndRetryPolicy(() => _httpClient.GetAsync($"{_appSettings.ApiEndpoint}/users"));

            return true;
        }

        public async Task<OperationResponse<AppConfigDto>> GetConfiguration()
        {
            //Now let's call our retry policy each time we want to query the API
            var response = await Policies.AttemptAndRetryPolicy(() => _httpClient.GetAsync($"{_appSettings.ApiEndpoint}/api/configuration"));

            return null;
        }
    }
}
