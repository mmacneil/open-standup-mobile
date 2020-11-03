using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using OpenStandup.Core.Domain.Features.Authenticate;
using OpenStandup.Core.Domain.Features.Authenticate.Models;
using OpenStandup.Core.Interfaces;
using OpenStandup.Core.Interfaces.Authentication;
using OpenStandup.Core.Interfaces.Data.Repositories;
using OpenStandup.SharedKernel;
using Xunit;


namespace OpenStandup.UnitTests.Authentication
{
    public class LoginUseCaseTests
    {
        private readonly Mock<IAuthenticator> _mockAuthenticator;
        private readonly Mock<ISecureDataRepository> _mockSecureDataRepository;
        private readonly Mock<IOutputPort<Result<string>>> _mockOutputPort;

        public LoginUseCaseTests()
        {
            _mockAuthenticator = new Mock<IAuthenticator>();
            _mockSecureDataRepository = new Mock<ISecureDataRepository>();
            _mockOutputPort = new Mock<IOutputPort<Result<string>>>();
        }

        [Fact]
        public async Task ShouldRemoveAccessTokenWhenAuthenticatorFails()
        {
            // arrange
            const string errorText = "login failed";

            _mockAuthenticator.Setup(x => x.Authenticate())
                .ReturnsAsync(Result<string>.Failed(errorText));

            _mockSecureDataRepository.Setup(x => x.SetPersonalAccessToken(""));

            var useCase = new LoginUseCase(_mockAuthenticator.Object, _mockSecureDataRepository.Object, _mockOutputPort.Object);

            // act
            var response = await useCase.Handle(new AuthenticationRequest(), new CancellationToken());

            // assert
            _mockSecureDataRepository.Verify(x => x.SetPersonalAccessToken(""), Times.Once());
            response.Errors.First().Should().Be(errorText);
        }


        [Fact]
        public async Task ShouldSetAccessTokenWhenAuthenticatorSucceeds()
        {
            // arrange
            const string token = "a1b2c3";

            _mockAuthenticator.Setup(x => x.Authenticate())
                .ReturnsAsync(Result<string>.Success(token));

            _mockSecureDataRepository.Setup(x => x.SetPersonalAccessToken(token));

            var useCase = new LoginUseCase(_mockAuthenticator.Object, _mockSecureDataRepository.Object, _mockOutputPort.Object);

            // act
            await useCase.Handle(new AuthenticationRequest(), new CancellationToken());

            // assert
            _mockSecureDataRepository.Verify(x => x.SetPersonalAccessToken(token), Times.Once());
        }
    }
}
