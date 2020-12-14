using OpenStandup.Core.Domain.Features.Login.Models;
using OpenStandup.Core.Interfaces.Authentication;
using OpenStandup.Core.Interfaces.Data.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using OpenStandup.Core.Interfaces;
using Vessel;

namespace OpenStandup.Core.Domain.Features.Login
{
    public class LoginUseCase : IRequestHandler<LoginRequest, Dto<string>>
    {
        private readonly IAuthenticator _authenticator;
        private readonly ISecureDataRepository _secureDataRepository;
        private readonly IOutputPort<Dto<string>> _outputPort;


        public LoginUseCase(IAuthenticator authenticator, ISecureDataRepository secureDataRepository, IOutputPort<Dto<string>> outputPort)
        {
            _authenticator = authenticator;
            _secureDataRepository = secureDataRepository;
            _outputPort = outputPort;
        }

        public async Task<Dto<string>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var authenticationResponse = await _authenticator.Authenticate().ConfigureAwait(false);

            if (authenticationResponse.Succeeded)
            {
                await _secureDataRepository.SetPersonalAccessToken(authenticationResponse.Payload).ConfigureAwait(false);
            }
            else
            {
                await _secureDataRepository.SetPersonalAccessToken("").ConfigureAwait(false);
            }

            await _outputPort.Handle(authenticationResponse);
            return authenticationResponse;
        }
    }
}
