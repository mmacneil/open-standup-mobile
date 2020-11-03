using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using OpenStandup.Core.Domain.Entities;
using OpenStandup.Core.Domain.Features.SaveProfile;
using OpenStandup.Core.Domain.Features.SaveProfile.Models;
using OpenStandup.Core.Interfaces;
using OpenStandup.Core.Interfaces.Data.GraphQL;
using OpenStandup.Core.Interfaces.Data.Repositories;
using OpenStandup.SharedKernel;
using OpenStandup.SharedKernel.Extensions;
using Xunit;


namespace OpenStandup.UnitTests.Profile
{
    public class GetGitHubProfileUseCaseTests
    {
        private readonly Mock<IGitHubGraphQLApi> _mockGitHubGraphQL;
        private readonly Mock<IOutputPort<Result<bool>>> _mockOutputPort;
        private readonly Mock<IProfileRepository> _mockProfileRepository;

        public GetGitHubProfileUseCaseTests()
        {
            _mockGitHubGraphQL = new Mock<IGitHubGraphQLApi>();
            _mockOutputPort = new Mock<IOutputPort<Result<bool>>>();
            _mockProfileRepository = new Mock<IProfileRepository>();
        }

        [Fact]
        public async Task ShouldNotPersistLocallyWhenGitHubApiCallFails()
        {
            // arrange
            _mockGitHubGraphQL.Setup(x => x.GetGitHubViewer())
                .ReturnsAsync(Result<GitHubUser>.Failed(HttpStatusCode.Unauthorized.ToResultStatus(), new Exception()));

            var useCase = new GetGitHubProfileUseCase(_mockGitHubGraphQL.Object, _mockProfileRepository.Object, null, _mockOutputPort.Object);

            // act
            var response = await useCase.Handle(new GetGitHubProfileRequest(), new CancellationToken());

            // assert
            _mockProfileRepository.Verify(x => x.InsertOrReplace(It.IsAny<GitHubUser>()), Times.Never);
            Assert.Equal(ResultStatus.Unauthorized, response.Status); // Received unauthorized status code from GQL call was mapped to result.
        }
    }
}
