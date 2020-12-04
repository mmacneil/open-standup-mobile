using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStandup.Common.Interfaces.Infrastructure;
using OpenStandup.Core.Domain.Features.Posts.Models;
using OpenStandup.Core.Interfaces;
using OpenStandup.Core.Interfaces.Apis;
using Vessel;


namespace OpenStandup.Core.Domain.Features.Posts
{
    public class PublishPostUseCase : IRequestHandler<PublishPostRequest>
    {
        private readonly IFileUtilities _fileUtilities;
        private readonly IOpenStandupApi _openStandupApi;
        private readonly IOutputPort<PublishPostResponse> _outputPort;

        public PublishPostUseCase(IFileUtilities fileUtilities, IOpenStandupApi openStandupApi, IOutputPort<PublishPostResponse> outputPort)
        {
            _fileUtilities = fileUtilities;
            _openStandupApi = openStandupApi;
            _outputPort = outputPort;
        }

        public async Task<Unit> Handle(PublishPostRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var apiResponse = await _openStandupApi.PublishPost(request.Text, await _fileUtilities.GetBytes(request.PhotoPath));

                if (apiResponse.Succeeded)
                {
                    _fileUtilities.TryDelete(request.PhotoPath);
                }

                await _outputPort.Handle(new PublishPostResponse(apiResponse)).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await _outputPort.Handle(new PublishPostResponse(Dto<bool>.Failed(e)));
            }

            return new Unit();
        }
    }
}