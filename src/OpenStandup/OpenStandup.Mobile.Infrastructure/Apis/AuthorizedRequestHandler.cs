using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStandup.Core.Domain.Events;


namespace OpenStandup.Mobile.Infrastructure.Apis
{
    public class AuthorizedRequestHandler : HttpClientHandler
    {
        private readonly IMediator _mediator;

        public AuthorizedRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await Policies.AttemptAndRetryPolicy(() => base.SendAsync(request, cancellationToken));

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await _mediator.Publish(new Unauthorized(response), cancellationToken).ConfigureAwait(false);
            }
            return response;
        }
    }
}
