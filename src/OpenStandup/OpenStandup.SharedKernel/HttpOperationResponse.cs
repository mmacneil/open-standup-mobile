using System.Net;


namespace OpenStandup.SharedKernel
{
    public class HttpOperationResponse<T> : OperationResponse<T>
    {
        public HttpStatusCode StatusCode { get; }

        public HttpOperationResponse(HttpStatusCode statusCode, T payload, string errorText = "") : base(
            statusCode == HttpStatusCode.OK ? OperationResult.Succeeded : OperationResult.Failed, payload, errorText)
        {
            StatusCode = statusCode;
        }
    }
}
