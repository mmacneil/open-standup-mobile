﻿using System.Threading.Tasks;
using OpenStandup.Common;
using OpenStandup.Common.Dto;
using OpenStandup.Core.Domain.Entities;
using Vessel;

namespace OpenStandup.Core.Interfaces.Apis
{
    public interface IOpenStandupApi
    {
        Task<Dto<bool>> UpdateProfile(GitHubUser gitHubUser);
        Task<Dto<AppConfigDto>> GetConfiguration();
        Task<Dto<string>> ValidateGitHubAccessToken(string token);
        Task<Dto<bool>> UpdateLocation(double latitude, double longitude);
        Task<Dto<bool>> PublishPost(string text, byte[] image);
        Task<Dto<PagedResult<PostSummaryDto>>> GetPostSummaries(int offset);
        Task<Dto<PostSummaryDto>> GetPost(int id);
        Task<Dto<GitHubUser>> GetUser(string gitHubId);
    }
}
