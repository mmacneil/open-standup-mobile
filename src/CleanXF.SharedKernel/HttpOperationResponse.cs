using System.Net;


namespace CleanXF.SharedKernel
{
    public class HttpOperationResponse<T> : OperationResponse<T>
    {
        public HttpStatusCode StatusCode { get; }

        public HttpOperationResponse(HttpStatusCode statusCode, OperationResult operationResult, T payload, string errorText = "") : base(operationResult, payload, errorText)
        {
            StatusCode = statusCode;
        }
    }
}
