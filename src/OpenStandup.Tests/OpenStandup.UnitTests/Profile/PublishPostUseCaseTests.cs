using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using OpenStandup.Common.Interfaces.Infrastructure;
using OpenStandup.Core.Domain.Features.Posts;
using OpenStandup.Core.Domain.Features.Posts.Models;
using OpenStandup.Core.Interfaces;
using OpenStandup.Core.Interfaces.Apis;
using Vessel;
using Xunit;

namespace OpenStandup.UnitTests.Profile
{
    public class PublishPostUseCaseTests
    {
        private readonly Mock<IFileUtilities> _mockFileUtilities;
        private readonly Mock<IOpenStandupApi> _mockApi;
        private readonly Mock<IOutputPort<PublishPostResponse>> _mockOutputPort;

        public PublishPostUseCaseTests()
        {
            _mockFileUtilities = new Mock<IFileUtilities>();
            _mockFileUtilities.Setup(x => x.GetBytes(null)).ReturnsAsync(Array.Empty<byte>());
            _mockApi = new Mock<IOpenStandupApi>();
            _mockOutputPort = new Mock<IOutputPort<PublishPostResponse>>();
        }

        [Fact]
        public async Task ShouldDeletePhotoWhenApiCallSucceeds()
        {
            _mockApi.Setup(x => x.PublishPost(null, It.IsAny<byte[]>())).ReturnsAsync(Dto<bool>.Success(true));
            var useCase = new PublishPostUseCase(_mockFileUtilities.Object, _mockApi.Object, _mockOutputPort.Object);
            await useCase.Handle(new PublishPostRequest(default, default), new CancellationToken());
            _mockFileUtilities.Verify(x => x.TryDelete(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ShouldCallPresenterWithSuccessResponseWhenAllOpsSucceed()
        {
            _mockApi.Setup(x => x.PublishPost(null, It.IsAny<byte[]>())).ReturnsAsync(Dto<bool>.Success(true));
            var useCase = new PublishPostUseCase(_mockFileUtilities.Object, _mockApi.Object, _mockOutputPort.Object);
            await useCase.Handle(new PublishPostRequest(default, default), new CancellationToken());
            _mockOutputPort.Verify(x => x.Handle(It.Is<PublishPostResponse>(p => p.ApiResponse.Succeeded)));
        }

        [Fact]
        public async Task ShouldCallPresenterWithExceptionWhenApiThrows()
        {
            const string error = "fatal error";
            _mockApi.Setup(x => x.PublishPost(null, It.IsAny<byte[]>())).ThrowsAsync(new InvalidOperationException(error));
            var useCase = new PublishPostUseCase(_mockFileUtilities.Object, _mockApi.Object, _mockOutputPort.Object);
            await useCase.Handle(new PublishPostRequest(default, default), new CancellationToken());
            _mockOutputPort.Verify(x =>
                x.Handle(It.Is<PublishPostResponse>(p =>
                    !p.ApiResponse.Succeeded &&
                    p.ApiResponse.Exception.Message == error)));
        }
    }
}
