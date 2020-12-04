using System.Net.Http;
using MediatR;

namespace OpenStandup.Core.Domain.Events
{
    public class Unauthorized : INotification
    {
        public HttpResponseMessage ResponseMessage { get; }

        public Unauthorized(HttpResponseMessage responseMessage)
        {
            ResponseMessage = responseMessage;
        }
    }
}
