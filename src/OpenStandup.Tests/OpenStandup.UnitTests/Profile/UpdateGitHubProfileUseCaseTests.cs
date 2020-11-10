using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using OpenStandup.Core.Domain.Entities;
using OpenStandup.Core.Domain.Features.Profile;
using OpenStandup.Core.Domain.Features.Profile.Models;
using OpenStandup.Core.Interfaces;
using OpenStandup.Core.Interfaces.Data.GraphQL;
using OpenStandup.Core.Interfaces.Data.Repositories;
using Vessel;
using Xunit;


namespace OpenStandup.UnitTests.Profile
{
    public class UpdateGitHubProfileUseCaseTests
    {
        private readonly Mock<IGitHubGraphQLApi> _mockGitHubGraphQL;
        private readonly Mock<IOutputPort<Dto<bool>>> _mockOutputPort;
        private readonly Mock<IProfileRepository> _mockProfileRepository;

        public UpdateGitHubProfileUseCaseTests()
        {
            _mockGitHubGraphQL = new Mock<IGitHubGraphQLApi>();
            _mockOutputPort = new Mock<IOutputPort<Dto<bool>>>();
            _mockProfileRepository = new Mock<IProfileRepository>();
        }

        [Fact]
        public async Task ShouldNotPersistLocallyWhenGQLApiCallFailsWithUnauthorizedException()
        {
            // arrange
            _mockGitHubGraphQL.Setup(x => x.GetGitHubViewer())
                .ReturnsAsync(Dto<GitHubUser>.Failed(HttpStatusCode.Unauthorized, new Exception()));

            var useCase = new UpdateGitHubProfileUseCase(_mockGitHubGraphQL.Object, _mockProfileRepository.Object, null, _mockOutputPort.Object, null);

            // act
            var response = await useCase.Handle(new UpdateGitHubProfileRequest(), new CancellationToken());

            // assert
            _mockProfileRepository.Verify(x => x.InsertOrReplace(It.IsAny<GitHubUser>()), Times.Never);
            Assert.Equal(Status.Unauthorized, response.Status); // Received unauthorized status code from GQL call was mapped to result.
        }

        [Fact]
        public async Task ShouldNotPersistLocallyWhenGQLApiCallFailsWithQueryErrors()
        {
            // arrange
            const string error = "query failure";
            _mockGitHubGraphQL.Setup(x => x.GetGitHubViewer())
                .ReturnsAsync(Dto<GitHubUser>.Failed(error));

            var useCase = new UpdateGitHubProfileUseCase(_mockGitHubGraphQL.Object, _mockProfileRepository.Object, null, _mockOutputPort.Object, null);

            // act
            var response = await useCase.Handle(new UpdateGitHubProfileRequest(), new CancellationToken());

            // assert
            _mockProfileRepository.Verify(x => x.InsertOrReplace(It.IsAny<GitHubUser>()), Times.Never);
            Assert.Equal(Status.Failed, response.Status); // Received unauthorized status code from GQL call was mapped to result.
            Assert.Equal(error, response.Errors.First());
        }
    }
}
