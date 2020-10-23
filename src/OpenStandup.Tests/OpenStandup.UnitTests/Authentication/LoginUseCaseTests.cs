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
        [Fact]
        public async Task ShouldRemoveAccessTokenWhenAuthenticatorFails()
        {
            // arrange
            const string errorText = "login failed";
            var mockAuthenticator = new Mock<IAuthenticator>();
            mockAuthenticator.Setup(x => x.Authenticate())
                .ReturnsAsync(new OperationResponse<string>(OperationResult.Failed, null, errorText));

            var mockSecureDataRepository = new Mock<ISecureDataRepository>();
            mockSecureDataRepository.Setup(x => x.SetPersonalAccessToken(""));

            var mockOutputPort = new Mock<IOutputPort<AuthenticationResponse>>();

            var useCase = new LoginUseCase(mockAuthenticator.Object, mockSecureDataRepository.Object);

            // act
            var response = await useCase.Handle(new AuthenticationRequest(mockOutputPort.Object), new CancellationToken());

            // assert
            mockSecureDataRepository.Verify(x => x.SetPersonalAccessToken(""), Times.Once());
            response.ErrorText.Should().Be(errorText);
        }

        [Fact]
        public async Task ShouldSetAccessTokenWhenAuthenticatorSucceeds()
        {
            // arrange
            const string token = "a1b2c3";
            var mockAuthenticator = new Mock<IAuthenticator>();
            mockAuthenticator.Setup(x => x.Authenticate())
                .ReturnsAsync(new OperationResponse<string>(OperationResult.Succeeded, token));

            var mockSecureDataRepository = new Mock<ISecureDataRepository>();
            mockSecureDataRepository.Setup(x => x.SetPersonalAccessToken(token));

            var mockOutputPort = new Mock<IOutputPort<AuthenticationResponse>>();

            var useCase = new LoginUseCase(mockAuthenticator.Object, mockSecureDataRepository.Object);

            // act
            await useCase.Handle(new AuthenticationRequest(mockOutputPort.Object), new CancellationToken());

            // assert
            mockSecureDataRepository.Verify(x => x.SetPersonalAccessToken(token), Times.Once());
        }
    }
}
