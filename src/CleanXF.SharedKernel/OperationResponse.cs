

namespace CleanXF.SharedKernel
{
    public class OperationResponse<T>
    {
        public OperationResult OperationResult { get; }
        public T Payload { get; set; }
        public string ErrorText { get; }

        public OperationResponse(OperationResult operationResult, T payload, string errorText = "")
        {
            OperationResult = operationResult;
            Payload = payload;
            ErrorText = errorText;
        }
    }
}
