using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStandup.Common.Interfaces.Infrastructure;
using OpenStandup.Core.Domain.Features.Posts.Models;
using OpenStandup.Core.Interfaces.Apis;
using Vessel;

namespace OpenStandup.Core.Domain.Features.Posts
{
    public class PublishPostUseCase : IRequestHandler<PublishPostRequest, Dto<bool>>
    {
        private readonly IFileUtilities _fileUtilities;
        private readonly IOpenStandupApi _openStandupApi;

        public PublishPostUseCase(IFileUtilities fileUtilities, IOpenStandupApi openStandupApi)
        {
            _fileUtilities = fileUtilities;
            _openStandupApi = openStandupApi;
        }

        public async Task<Dto<bool>> Handle(PublishPostRequest request, CancellationToken cancellationToken)
        {
            var image = await _fileUtilities.GetBytes(request.PhotoPath).ConfigureAwait(false);
            await _openStandupApi.PublishPost(request.Text, image);
            return Dto<bool>.Success(true);
        }
    }
}