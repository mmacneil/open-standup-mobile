using CleanXF.SharedKernel;

namespace CleanXF.Core.Domain.Features.Authenticate.Models
{
    public class AuthenticationResponse : OperationResponse<string>
    {
        public AuthenticationResponse(OperationResult operationResult, string accessToken, string errorText = "") : base(operationResult, accessToken, errorText)
        {
        }
    }
}
