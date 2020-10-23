using System;
using System.Net;

namespace OpenStandup.SharedKernel
{
    public class GraphQLOperationResponse<T> : OperationResponse<T>
    {
        public HttpStatusCode StatusCode { get; }
        public Exception Exception { get; }
        public bool Unauthorized => StatusCode == HttpStatusCode.Unauthorized;

        public GraphQLOperationResponse(T payload) : base(OperationResult.Succeeded, payload, null)
        {
        }

        public GraphQLOperationResponse(HttpStatusCode statusCode, Exception exception) : base(OperationResult.Failed, default, null)
        {
            StatusCode = statusCode;
            Exception = exception;
        }
    }
}
