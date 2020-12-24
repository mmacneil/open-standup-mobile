using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStandup.Common.Extensions;
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
        private readonly IImageUtilities _imageUtilities;
        private readonly IOpenStandupApi _openStandupApi;
        private readonly IOutputPort<PublishPostResponse> _outputPort;

        public PublishPostUseCase(IFileUtilities fileUtilities, IImageUtilities imageUtilities, IOpenStandupApi openStandupApi, IOutputPort<PublishPostResponse> outputPort)
        {
            _fileUtilities = fileUtilities;
            _imageUtilities = imageUtilities;
            _openStandupApi = openStandupApi;
            _outputPort = outputPort;
        }

        public async Task<Unit> Handle(PublishPostRequest request, CancellationToken cancellationToken)
        {
            try
            {
                byte[] imageBytes = null;
                var rawImageBytes = await _fileUtilities.GetBytes(request.PhotoPath).ConfigureAwait(false);
                
                if (!rawImageBytes.IsNullOrEmpty())
                {
                    // Downsize raw image
                    imageBytes = _imageUtilities.Resize(rawImageBytes, 2272, 1704);
                }
                var apiResponse = await _openStandupApi.PublishPost(request.Text, imageBytes);

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