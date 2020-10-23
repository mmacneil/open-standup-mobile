using OpenStandup.SharedKernel;

namespace OpenStandup.Core.Domain.Features.Authenticate.Models
{
    public class AuthenticationResponse : OperationResponse<string>
    {
        public AuthenticationResponse(OperationResult operationResult, string errorText = null) : base(operationResult, null, errorText)
        {
        }
    }
}
