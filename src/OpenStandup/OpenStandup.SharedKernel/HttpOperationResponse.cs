using System.Net;
using System.Net.Http;


namespace OpenStandup.SharedKernel
{
    public class HttpOperationResponse<T> : OperationResponse<T>
    {
        public HttpStatusCode StatusCode { get; }
        public HttpResponseMessage HttpResponseMessage { get; }
        public bool Unauthorized => StatusCode == HttpStatusCode.Unauthorized;

        public HttpOperationResponse(HttpStatusCode statusCode, HttpResponseMessage httpResponseMessage, T payload, string errorText = "") : base(
            statusCode == HttpStatusCode.OK ? OperationResult.Succeeded : OperationResult.Failed, payload, errorText)
        {
            StatusCode = statusCode;
            HttpResponseMessage = httpResponseMessage;
        }
    }
}
